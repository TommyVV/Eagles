
namespace Eagles.Application.Model.Task.EditTaskStep
{
    /// <summary>
    /// 任务计划编辑
    /// </summary>
    public class EditTaskStepRequest : RequestBase
    {
        /// <summary>
        /// 支部Id
        /// </summary>
        public int BranchId { get; set; }

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
        public string StepName { get; set; }
        
    }
}