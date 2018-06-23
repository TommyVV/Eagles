using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Task.EditTaskStep
{
    /// <summary>
    /// 任务计划编辑
    /// </summary>
    public class EditTaskStepRequest : RequestBase
    {
        /// <summary>
        /// 操作类型 增、删、改
        /// </summary>
        public ActionEnum Action { get; set; }

        /// <summary>
        /// 支部Id
        /// </summary>
        public string BranchId { get; set; }

        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 计划Id
        /// </summary>
        public int StepId { get; set; }

        /// <summary>
        /// 步骤内容
        /// </summary>
        public string StepContent { get; set; }
        
    }
}