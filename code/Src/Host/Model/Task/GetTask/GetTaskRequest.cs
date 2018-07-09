using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Task.GetTask
{
    /// <summary>
    /// 任务查询
    /// </summary>
    public class GetTaskRequest : QueryRequestBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 0-发起的任务 1-负责的任务
        /// </summary>
        public TaskEnum TaskType { get; set; }
    }
}