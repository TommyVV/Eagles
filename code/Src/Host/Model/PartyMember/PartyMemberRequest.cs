using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.PartyMember
{
    public class PartyMemberRequest : RequestBase
    {
        public UserIdentity UserIdentity { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 机构编号
        /// </summary>
        public string OrgName { get; set; }

    }
}
