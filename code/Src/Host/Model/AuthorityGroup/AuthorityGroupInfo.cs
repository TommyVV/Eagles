using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.AuthorityGroup
{
    /// <summary>
    /// 权限组对象
    /// </summary>
    public class AuthorityGroupInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int AuthorityGroupId { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string AuthorityGroupName { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        
        /// <summary>
        /// 权限组状态
        /// </summary>
        public Status Status { get; set; }
        
    }
}
