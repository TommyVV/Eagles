using System;

namespace Eagles.Application.Model.Curd.Activity.GetActivityComment
{
    /// <summary>
    /// 活动评论查询
    /// </summary>
    public class GetActivityCommentRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }
    }
}