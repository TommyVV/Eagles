using System;

namespace Eagles.Application.Model.Enums
{
    /// <summary>
    /// 任务选项 0-发起的任务 1-负责的任务
    /// </summary>
    public enum TaskEnum
    {
        /// <summary>
        /// 发起的任务
        /// </summary>
        From = 0,
        /// <summary>
        /// 负责的任务
        /// </summary>
        To = 1
    }
}