using System.Collections.Generic;

namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAuthorityUserSetUp : RequestBase
    {
        /// <summary>
        /// 当前用户userId
        /// </summary>
        public  int UserId { get; set; }

        /// <summary>
        /// 下级
        /// </summary>
        public List<int> UserIds { get; set; }
    }
}
