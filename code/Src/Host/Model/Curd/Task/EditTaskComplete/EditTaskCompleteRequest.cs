using System;

namespace Eagles.Application.Model.Curd.Task.EditTaskComplete
{
    /// <summary>
    /// 任务完成接口
    /// </summary>
    public class EditTaskCompleteRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public string IsPublic { get; set; }

        /// <summary>
        /// 任务完成/审核
        /// </summary>
        public string TaskType { get; set; }

        /// <summary>
        /// 任务评分
        /// </summary>
        public string TaskSorce { get; set; }
    }
}