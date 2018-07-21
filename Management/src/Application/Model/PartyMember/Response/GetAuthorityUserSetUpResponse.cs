using System.Collections.Generic;
using Eagles.Application.Model.PartyMember.Model;

namespace Eagles.Application.Model.PartyMember.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetAuthorityUserSetUpResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 下级用户id集合
        /// </summary>
        public List<User> User { get; set; }
    }
}
