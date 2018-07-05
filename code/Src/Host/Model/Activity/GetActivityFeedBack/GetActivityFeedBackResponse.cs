using System.Collections.Generic;

namespace Eagles.Application.Model.Activity.GetActivityFeedBack
{
    /// <summary>
    /// 活动反馈列表
    /// </summary>
    public class GetActivityFeedBackResponse : ResponseBase
    {
        /// <summary>
        /// 反馈列表
        /// </summary>
        public List<Common.FeedBack> FeedBackList { get; set; }
    }
}