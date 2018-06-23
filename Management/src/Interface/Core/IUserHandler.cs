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
        ResponseBase RemoveUserInfoDetails(RemoveUserInfoDetailsRequest request);
        ResponseBase EditUserInfoDetails(EditUserInfoDetailsRequest request);
        GetAuthorityUserSetUpResponse GetAuthorityUserSetUp(GetAuthorityUserSetUpRequset requset);
        ResponseBase CreateAuthorityUserSetUp(CreateAuthorityUserSetUp requset);
        ResponseBase RemoveAuthorityUserSetUp(RemoveAuthorityUserSetUp requset);
    }
}
