using System;

namespace Eagles.Application.Model.Curd.Task.EditTaskAccept
{
    /// <summary>
    /// 任务接受接口
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
        public int TaskId { get; set; }
        
    }
}