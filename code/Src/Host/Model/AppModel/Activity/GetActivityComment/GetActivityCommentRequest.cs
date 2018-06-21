using System;

namespace Eagles.Application.Model.AppModel.Activity.GetActivityComment
{
    /// <summary>
    /// 活动评论查询
    /// </summary>
    public class GetActivityCommentRequest : RequestBase
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }
    }
}