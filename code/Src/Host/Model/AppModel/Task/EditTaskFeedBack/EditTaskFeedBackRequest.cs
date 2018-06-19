using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.AppModel.Task.EditTaskFeedBack
{
    /// <summary>
    /// 任务反馈接口
    /// </summary>
    public class EditTaskFeedBackRequest : RequestBase
    {
        /// <summary>
        /// 任务Id
        /// </summary>
        public int TaskId { get; set; }

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