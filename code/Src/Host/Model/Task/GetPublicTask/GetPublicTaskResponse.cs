using System.Collections.Generic;

namespace Eagles.Application.Model.Task.GetPublicTask
{
    /// <summary>
    /// 公开任务查询
    /// </summary>
    public class GetPublicTaskResponse 
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        public List<Common.Task> TaskList { get; set; }
    }
}