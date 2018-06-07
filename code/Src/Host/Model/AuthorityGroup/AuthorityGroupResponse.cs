using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.AuthorityGroup
{
    /// <summary>
    /// 权限组列表返回
    /// </summary>
    public class AuthorityGroupResponse:ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<AuthorityGroupInfo> List { get; set; }
    }
}
