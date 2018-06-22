using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Application.Model.PartyMember.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IPartyMemberHandler : IInterfaceBase
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
