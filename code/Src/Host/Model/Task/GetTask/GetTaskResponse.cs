using System.Collections.Generic;

namespace Eagles.Application.Model.Task.GetTask
{
    /// <summary>
    /// 任务查询
    /// </summary>
    public class GetTaskResponse : ResponseBase
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        public List<Common.Task> TaskList { get; set; }
    }
}