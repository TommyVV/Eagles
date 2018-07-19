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
using Eagles.Application.Model.PartyMember.Model;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
   public class MettingHandler: IMettingHandler
    {
        private readonly IMettingDataAccess dataAccess;

        private readonly INewsDataAccess NewdataAccess;
        public MettingHandler(IMettingDataAccess dataAccess, INewsDataAccess newdataAccess)
        {
            this.dataAccess = dataAccess;
            NewdataAccess = newdataAccess;
        }

        public ImportMeetingResponse ImportMeetUserInfoRequset(ImportMeetUserInfoRequset requset)
        {

            var response = new ImportMeetingResponse {ImportUsersResult = new List<MeetUser>()};
            Regex rx = new Regex(@"^0{0,1}(13[4-9]|15[7-9]|15[0-2]|18[7-8])[0-9]{8}$");
            var userinfo = new List<TbMeetingUser>();
            var list = new List<string>();

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

            userinfo = user.Select(x => new TbMeetingUser()
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
            List<TbMeetingUser> list = dataAccess.GetMettingUsers(requset, out int totalCount) ??
                                       new List<TbMeetingUser>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

           // NewdataAccess.GetNewsList(list)

            // var orginfo = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;
            response.List = list.Select(x => new Meeting
            {
                MeetingId = x.NewsId,
                //MeetingInitiator=x.MeetingInitiator,
                //MeetingNmae=x.MeetingNmae,
                Participants = new List<string>()
            }).ToList();
            return response;

        }
    }
}
