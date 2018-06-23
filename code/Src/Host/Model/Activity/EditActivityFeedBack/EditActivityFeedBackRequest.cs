using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Activity.EditActivityFeedBack
{
    /// <summary>
    /// 活动反馈接口
    /// </summary>
    public class EditActivityFeedBackRequest : RequestBase
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> AttachList { get; set; }
    }
}