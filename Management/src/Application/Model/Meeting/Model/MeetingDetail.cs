using System;
using System.Collections.Generic;

namespace Eagles.Application.Model.Meeting.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MeetingDetail: Meeting
    {
        /// <summary>
        /// 发起开始时间
        /// </summary>
        public DateTime LaunchStartTime { get; set; }

        /// <summary>
        /// 发起结束时间
        /// </summary>
        public DateTime LaunchEndTime { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 附件 json格式
        /// </summary>
        public string Enclosure { get; set; }

        /// <summary>
        /// 参与人员
        /// </summary>
        public List<MeetUserDetail> List { get; set; }

    }
}
