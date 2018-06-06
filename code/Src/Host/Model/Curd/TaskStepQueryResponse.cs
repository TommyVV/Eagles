using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 任务步骤查询
    /// </summary>
    class TaskStepQueryResponse : ResponseBase
    {
        public List<Step> StepList { get; }
    }

    class Step
    {
        public string StepId { get; set; }
        public string StepContent { get; set; }
    }
}
