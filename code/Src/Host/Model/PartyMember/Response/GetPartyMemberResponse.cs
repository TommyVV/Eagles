using System.Collections.Generic;
using Eagles.Application.Model.PartyMember.Model;

namespace Eagles.Application.Model.PartyMember.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetPartyMemberResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<UserInfo> List { get; set; }
    }
}
