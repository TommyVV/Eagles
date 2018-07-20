using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Eagles.Application.Model.Meeting.Model;
using Eagles.Application.Model.Meeting.Requset;
using Eagles.Application.Model.Meeting.Response;
using Eagles.Application.Model.Menus.Model;
using Eagles.Application.Model.News.Requset;
using Eagles.Application.Model.PartyMember.Model;
using Eagles.Base;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;
using OfficeOpenXml.FormulaParsing.Utilities;

namespace Eagles.DomainService.Core
{
   public class MettingHandler: IMettingHandler
    {
        private readonly IMettingDataAccess dataAccess;

        private readonly INewsDataAccess NewdataAccess;

        private readonly IPartyMemberDataAccess UserdataAccess;
        public MettingHandler(IMettingDataAccess dataAccess, INewsDataAccess newdataAccess, IPartyMemberDataAccess userdataAccess)
        {
            this.dataAccess = dataAccess;
            NewdataAccess = newdataAccess;
            UserdataAccess = userdataAccess;
        }

        public ImportMeetingResponse ImportMeetUserInfoRequset(ImportMeetUserInfoRequset requset)
        {

            var response = new ImportMeetingResponse {ImportUsersResult = new List<MeetUser>()};
            Regex rx = new Regex(@"^0{0,1}(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$");
            var list = new List<string>();

            var news = NewdataAccess.GetNewsDetail(new GetNewDetailRequset()
            {
                NewsId=requset.MeetingId
            });

            if (news.NewsType != 1)
            {
                throw new TransactionException("", "不存在该会议内容!");
            }

            foreach (var md in requset.List)
            {
                if (!rx.IsMatch(md.MeetUserPhone))
                {
                    md.ErrorMessage = "手机号格式错误！";
                    response.ImportUsersResult.Add(md);
                    break;
                }

                list.Add(md.MeetUserPhone);
            }

            List<TbUserInfo> user = dataAccess.GetUserInfoByPhone(list);

            var userinfo = user.Select(x => new TbMeetingUser()
            {
                BranchId = x.BranchId,
                NewsId = requset.MeetingId,
                OrgId = x.OrgId,
                UserId = x.UserId
            }).ToList();


            if (dataAccess.CreateUserInfo(userinfo) <= 0)
            {
                response.ImportUsersResult.AddRange(userinfo.Select(x => new MeetUser()
                {
                    MeetUserName = user.FirstOrDefault(f => f.UserId == x.UserId)?.Name,
                    MeetUserPhone = user.FirstOrDefault(f => f.UserId == x.UserId)?.Phone
                }));
            }

            return response;

        }

        public GetMeetingResponse GetMettingUsers(GetMeetingRequest requset)
        {
            var response = new GetMeetingResponse
            {
                TotalCount = 0,
            };
            List<TbMeetingUser> list = dataAccess.GetMettingUsers(requset) ??
                                       new List<TbMeetingUser>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            List<TbNews> news = NewdataAccess.GetNewsList(list.Select(x => x.NewsId).ToList());

          //  List<TbUserInfo> userInfo = UserdataAccess.GetUserInfoList(list.Select(x => x.UserId).ToList());
            // var orginfo = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            //组装返回数据
            response.List = list.GroupBy(g => new {g.NewsId}).Select(f => new Meeting
            {
                MeetingId = f.Key.NewsId,
                MeetingInitiator = news.FirstOrDefault(x => x.NewsId == f.Key.NewsId)?.Author,
                MeetingNmae = news.FirstOrDefault(x => x.NewsId == f.Key.NewsId)?.Title,
                Participants =
                    UserdataAccess
                        .GetUserInfoList(list.Where(x => x.NewsId == f.Key.NewsId).Select(x => x.UserId).ToList())
                        .Select(x => x.Name).ToList()
            }).ToList();


            //新闻名字条件筛选
            if (!string.IsNullOrWhiteSpace(requset.MeetingNmae))
            {

                response.List = response.List.Where(x => x.MeetingNmae == requset.MeetingNmae).ToList();

            }
           
            return response;

        }
    }
}
