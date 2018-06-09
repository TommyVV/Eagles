using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.AuthorityGroupSetUp
{
    /// <summary>
    /// 设置权限组 权限
    /// </summary>
    public class AuthorityGroupSetUpRequset
    {
        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public int AuthorityId { get; set; }

        /// <summary>
        /// 设置的权限list
        /// </summary>
        public List<AuthorityInfo> AuthorityInfo { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}
