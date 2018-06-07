using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit
{
    public class AuditInfo
    {
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


        public AuditStatus AuditStatus { get; set; }
    }
}
