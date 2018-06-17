using System.Collections.Generic;
using Eagles.Application.Model.AuthorityGroupSetUp.Model;

namespace Eagles.Application.Model.AuthorityGroupSetUp.Requset
{
    /// <summary>
    /// 设置权限组 权限
    /// </summary>
    public class EditAuthorityGroupSetUpRequset:OrgRequestBase
    {
       

        /// <summary>
        /// 主键
        /// </summary>
        public int AuthorityId { get; set; }

        /// <summary>
        /// 设置的权限list
        /// </summary>
        public List<AuthorityInfo> AuthorityInfo { get; set; }
    }
}
