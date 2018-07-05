using System.Collections.Generic;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 反馈
    /// </summary>
    public class FeedBack
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 反馈
        /// </summary>
        public string UserFeedBack { get; set; }

        /// <summary>
        /// 附件列表
        /// </summary>
        public List<Attachment> AttachList { get; set; }
    }
}