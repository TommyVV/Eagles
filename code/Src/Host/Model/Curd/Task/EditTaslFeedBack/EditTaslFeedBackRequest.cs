using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Task.EditTaslFeedBack
{
    /// <summary>
    /// 任务反馈接口
    /// </summary>
    public class EditTaslFeedBackRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 任务Id
        /// </summary>
        public string TaskId { get; set; }

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