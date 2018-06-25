using System;

namespace Eagles.DomainService.Model.Oper
{
    /// <summary>
    /// TB_OPER_GROUP
    /// </summary>
    public class TbOperGroup
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 权限组编号
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 管理组名称
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
    }
}