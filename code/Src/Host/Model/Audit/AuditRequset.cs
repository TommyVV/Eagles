using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Audit
{
    public class AuditRequset:RequestBase
    {
        /// <summary>
        /// 审核名字
        /// </summary>
        public string AuditName { get; set; }

        public AuditStatus AuditStatus { get; set; }

        public OperationType OperationType { get; set; }
    }
}
