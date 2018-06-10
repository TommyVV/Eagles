using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Meeting
{
    public class MeetingInfoDetails: MeetingInfo
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
        public List<MeetUserInfoDetails> List { get; set; }

    }
}
