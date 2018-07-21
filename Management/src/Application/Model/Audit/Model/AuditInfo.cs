using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Audit.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditInfo:RequestBase
    {
        public AuditType AuditType { get; set; }

        public int AuditId { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public enum AuditType
    {
        新闻 = 1,
        用户 = 2,
        产品 = 3,
        活动 = 4,
        任务 = 5
    }
}
