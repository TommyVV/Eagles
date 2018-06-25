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
        /// 权限组编号
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 设置的权限list
        /// </summary>
        public List<AuthorityInfo> AuthorityInfo { get; set; }
    }
}
