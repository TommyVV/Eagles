using System.Collections.Generic;

namespace Eagles.Application.Model.Activity.GetActivityFeedBack
{
    /// <summary>
    /// 活动查询
    /// </summary>
    public class GetActivityFeedBackResponse : ResponseBase
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        public List<Common.Activity> ActivityList { get; set; }
    }
}