using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.AuthorityGroup
{
    /// <summary>
    /// 权限组列表请求
    /// </summary>
    public class AuthorityGroupRequset:RequestBase
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string AuthorityName { get; set; }
    }
}
