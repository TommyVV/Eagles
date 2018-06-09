using System;

namespace Eagles.Application.Model.Curd.Task.GetTask
{
    /// <summary>
    /// 任务查询
    /// </summary>
    public class GetTaskRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 加密用户编号
        /// </summary>
        public string EncryptUserid { get; set; }
    }
}