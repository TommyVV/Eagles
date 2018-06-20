using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.PartyMember;

namespace Eagles.Interface.Core.DataBase
{
    public interface IPartyMemberDataAccess : IInterfaceBase
    {
      //  List<TB_USER_INFO> GetUserInfoList(GetPartyMemberRequest requset, out int totalCount);
        TB_USER_INFO GetUserInfoDetail(GetUserInfoDetailRequest requset);
        int RemoveUserInfo(RemoveUserInfoDetailsRequest requset);
        int EditUserInfo(TB_USER_INFO info);
        int CreateUserInfo(TB_USER_INFO info);
        List<TB_USER_INFO> GetUserInfoList(GetPartyMemberRequest request, out int totalCount);
        List<TB_USER_INFO> GetUserInfoList(GetAuthorityUserSetUpRequset requset, out int totalCount);
        int RemoveAuthorityUserSetUp(List<TB_USER_RELATIONSHIP> list);
        int CreateAuthorityUserSetUp(List<TB_USER_RELATIONSHIP> list);
        List<TB_USER_RELATIONSHIP> GetAuthorityUserSetUp(int requsetUserId);
    }
}
