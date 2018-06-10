using System;

namespace Eagles.Application.Model.Curd.Task.EditTaskAccept
{
    /// <summary>
    /// 任务完成接口
    /// </summary>
    public class EditTaskAcceptRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }
        
    }
}