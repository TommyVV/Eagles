using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.PartyMember.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Base.Configuration;
using Eagles.DomainService.Model.PartyMember;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core
{
    public class PartyMemberHandler: IPartyMemberHandler
    {
        private readonly IPartyMemberDataAccess dataAccess;

        private readonly IOrganizationDataAccess OrgdataAccess;

        private readonly IConfigurationManager configurationManager;

        public GetPartyMemberResponse GetPartyMemberList(GetPartyMemberRequest request)
        {

            var response = new GetPartyMemberResponse
            {
                ErrorCode = "00",
                IsSuccess = true,
                Message = "成功",
            };
            //  //得到试卷 + 习题的关系
            List<TB_USER_INFO> list = dataAccess.GetUserInfoList(request, out int totalCount);

            if (list.Count == 0) throw new Exception("无数据");


            response.TotalCount = totalCount;
            response.List = list.Select(x => new UserInfo
            {
                OrgName = "",
                Phone = x.Phone,
                UserId = x.UserId,
                UserName = x.Name,
            }).ToList();


            //  //数据库计算 派出已关联的 
            //  List<TB_QUESTION> subjectList =
            //      dataAccess.GetRandomSubject(list.Select(x => x.QuestionId).ToList(), requset.RandomSubjectSum);

            //  response.SubjectList = subjectList.Select(f => new Subject
            //  {
            //      Question = f.Question,
            //      QuestionId = f.QuestionId
            //  }).ToList();

            return response;
            
        }

        public GetUserInfoDetailResponse GetUserInfoDetail(GetUserInfoDetailRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase RemoveUserInfoDetails(RemoveUserInfoDetailsRequest request)
        {
            throw new NotImplementedException();
        }

        public ResponseBase EditUserInfoDetails(EditUserInfoDetailsRequest request)
        {
            throw new NotImplementedException();
        }

        public GetAuthorityUserSetUpResponse GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset)
        {
            throw new NotImplementedException();
        }

        public ResponseBase EditAuthorityUserSetUp(EditAuthorityUserSetUpRequset requset)
        {
            throw new NotImplementedException();
        }
    }
}
