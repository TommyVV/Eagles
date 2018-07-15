
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
        /// 任务内容
        /// </summary>
        public string TaskContent { get; set; }

        /// <summary>
        /// 任务日期
        /// </summary>
        public string TaskDate { get; set; }

        /// <summary>
        /// 任务日期
        /// </summary>
        public int TaskStatus { get; set; }

        /// <summary>
        /// 任务发起人
        /// </summary>
        public int TaskFromUser { get; set; }

        /// <summary>
        /// 任务发起人名称
        /// </summary>
        public string TaskFromUserName { get; set; }

        /// <summary>
        /// 任务接受人
        /// </summary>
        public int TaskToUser { get; set; }

        /// <summary>
        /// 任务接受人名称
        /// </summary>
        public string TaskToUserName { get; set; }
    }
}