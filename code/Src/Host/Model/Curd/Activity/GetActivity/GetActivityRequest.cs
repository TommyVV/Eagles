using System;
using Eagles.Application.Model.Curd.Enum;

namespace Eagles.Application.Model.Curd.Activity.GetActivity
{
    /// <summary>
    /// 活动查询
    /// </summary>
    public class GetActivityRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
        
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 活动类型;0:报名;1:投票;2:问卷调查
        /// </summary>
        public ActivityType ActivityType { get; set; }
    }
}