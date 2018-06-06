using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class TaskStepOperationRequest : RequestBase
    {
        public string Token { get; set; }
        /// <summary>
        /// 操作类型 增、删、改
        /// </summary>
        public string OperType { get; set; }
        public string BranchId { get; set; }
        public string StepId { get; set; }
        public string StepContent { get; set; }
        
    }
}
