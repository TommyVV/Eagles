using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 用户通知
    /// </summary>
    public class UserNotice
    {
        /// <summary>
        /// 通知Id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 通知内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否已阅读;0:已阅读1:未阅读
        /// </summary>
        public int IsRead { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 接收用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 发送人
        /// </summary>
        public int FromUser { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string TargetUrl { get; set; }
    }
}