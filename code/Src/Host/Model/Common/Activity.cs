using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 活动
    /// </summary>
    public class Activity
    {
        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityType ActivityType { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        
        /// <summary>
        /// 活动内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 活动日期
        /// </summary>
        public DateTime ActivityDate { get; set; }
        
        /// <summary>
        /// 试卷编号
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// 活动图片Url
        /// </summary>
        public string ImageUrl { get; set; }
    }
}