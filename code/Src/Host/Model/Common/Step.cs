using System.Collections.Generic;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 步骤
    /// </summary>
    public class Step
    {
        /// <summary>
        /// 步骤Id
        /// </summary>
        public int StepId { get; set; }

        /// <summary>
        /// 步骤内容
        /// </summary>
        public string StepName { get; set; }

        /// <summary>
        /// 反馈内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 反馈附件列表
        /// </summary>
        public List<Attachment> AttachList { get; set; }
    }
}