namespace Eagles.Application.Model.Task.EditTaskComplete
{
    /// <summary>
    /// 任务完成接口
    /// </summary>
    public class EditTaskCompleteRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }

        /// <summary>
        /// 审核状态 0-通过 1-不通过
        /// </summary>
        public int CompleteStatus { get; set; }

    }
}