using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Audit
    {
        /// <summary>
        /// 
        /// </summary>
        public int AuditId { get; set; }

        /// <summary>
        /// 审核名称
        /// </summary>
        public int AuditName { get; set; }

        /// <summary>
        /// 发起人名字
        /// </summary>
        public int UserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public AuditStatus AuditStatus { get; set; }
    }
}
