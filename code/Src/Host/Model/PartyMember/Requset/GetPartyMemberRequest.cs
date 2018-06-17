using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetPartyMemberRequest : OrgListRequestBase
    {
        /// <summary>
        /// 
        /// </summary>
        public UserIdentity UserIdentity { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

    }
}
