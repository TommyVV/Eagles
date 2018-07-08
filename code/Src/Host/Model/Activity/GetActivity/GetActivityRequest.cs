using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Activity.GetActivity
{
    /// <summary>
    /// 活动查询
    /// </summary>
    public class GetActivityRequest : QueryRequestBase
    {
        /// <summary>
        /// 活动类型;0:全部；1：报名;2:投票;3:问卷调查
        /// </summary>
        public ActivityType ActivityType { get; set; }

        /// <summary>
        /// 0-所有活动 1-我参加的 2-待我参加的
        /// </summary>
        public ActivityPage ActivityPage { get; set; }
    }
}