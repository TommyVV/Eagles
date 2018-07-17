using System.Collections.Generic;

namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveAuthorityUserSetUp : RequestBase
    {
        /// <summary>
        /// 当前用户userId
        /// </summary>
        public  int UserId { get; set; }
        /// <summary>
        /// 下级用户id
        /// </summary>
        public List<int> UserIds { get; set; }
    }
}
