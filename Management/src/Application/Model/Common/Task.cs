using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 任务
    /// </summary>
    public class Task
    {
        /// <summary>
        /// 任务编号
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string TaskeName { get; set; }

        /// <summary>
        /// 任务日期
        /// </summary>
        public DateTime TaskDate { get; set; }

        /// <summary>
        /// 任务日期
        /// </summary>
        public int TaskStatus { get; set; }

        /// <summary>
        /// 任务发起人
        /// </summary>
        public int TaskFromUser { get; set; }
    }
}