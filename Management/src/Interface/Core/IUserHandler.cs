using Eagles.Application.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IUserHandler : IInterfaceBase
    {
        GetPartyMemberResponse GetPartyMemberList(GetPartyMemberRequest request);
        GetUserInfoDetailResponse GetUserInfoDetail(GetUserInfoDetailRequest request);
        bool RemoveUserInfoDetails(RemoveUserInfoDetailsRequest request);
        bool EditUserInfoDetails(EditUserInfoDetailsRequest request);
        GetAuthorityUserSetUpResponse GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset);
        bool CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset);
        bool RemoveAuthorityUserSetUp(RemoveAuthorityUserSetUp requset);

        ImportUserResponse BatchImportUser(ImportUserRequest request);
    }
}
