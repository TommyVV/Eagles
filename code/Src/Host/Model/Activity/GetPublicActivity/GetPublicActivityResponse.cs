using System.Collections.Generic;

namespace Eagles.Application.Model.Activity.GetPublicActivity
{
    /// <summary>
    /// 公开活动查询
    /// </summary>
    public class GetPublicActivityResponse 
    {
        /// <summary>
        /// 活动列表
        /// </summary>
        public List<Common.Activity> ActivityList { get; set; }
    }
}