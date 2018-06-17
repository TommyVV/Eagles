using System.Collections.Generic;

namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditAuthorityUserSetUpRequset : RequestBase
    {
        /// <summary>
        /// 下级
        /// </summary>
        public List<int> UserIds { get; set; }
    }
}
