using System.Collections.Generic;

namespace Eagles.Application.Model.Activity.GetActivity
{
    /// <summary>
    /// 活动查询
    /// </summary>
    public class GetActivityResponse : ResponseBase
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        public List<Common.Activity> ActivityList { get; set; }
    }
}