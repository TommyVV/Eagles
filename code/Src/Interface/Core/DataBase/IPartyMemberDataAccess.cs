using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.Application.Model.PartyMember.Requset;

namespace Eagles.Interface.Core.DataBase
{
    public interface IPartyMemberDataAccess : IInterfaceBase
    {
      //  List<TbUserInfo> GetUserInfoList(GetPartyMemberRequest requset, out int totalCount);
        TbUserInfo GetUserInfoDetail(GetUserInfoDetailRequest requset);
        int RemoveUserInfo(RemoveUserInfoDetailsRequest requset);
        int EditUserInfo(TbUserInfo info);
        int CreateUserInfo(TbUserInfo info);
        List<TbUserInfo> GetUserInfoList(GetPartyMemberRequest request, out int totalCount);
        List<TbUserInfo> GetUserInfoList(GetAuthorityUserSetUpRequset requset, out int totalCount);
        int RemoveAuthorityUserSetUp(List<TbUserRelationship> list);
        int CreateAuthorityUserSetUp(List<TbUserRelationship> list);
        List<TbUserRelationship> GetAuthorityUserSetUp(int requsetUserId);
    }
}