using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.DomainService.Model.PartyMember
{
    public class TB_USER_RELATIONSHIP
    {
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 领导id
        /// </summary>
        public int SubUserId { get; set; }
        /// <summary>
        /// 下级id
        /// </summary>
        public int UserId { get; set; }
    }
}
