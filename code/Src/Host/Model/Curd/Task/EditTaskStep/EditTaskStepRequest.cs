using System;
using Eagles.Application.Model.Curd.Enum;

namespace Eagles.Application.Model.Curd.Task.EditTaskStep
{
    /// <summary>
    /// 任务步骤编辑
    /// </summary>
    public class EditTaskStepRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 操作类型 增、删、改
        /// </summary>
        public ActionEnum Action { get; set; }

        /// <summary>
        /// 支部Id
        /// </summary>
        public string BranchId { get; set; }

        /// <summary>
        /// 步骤Id
        /// </summary>
        public int StepId { get; set; }

        /// <summary>
        /// 步骤内容
        /// </summary>
        public string StepContent { get; set; }
        
    }
}