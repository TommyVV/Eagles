using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.PartyMember
{
    public class AuthorityUserSetUpRequset:RequestBase
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 是否关联
        /// </summary>
        public bool IsRelation { get; set; }
    }
}
