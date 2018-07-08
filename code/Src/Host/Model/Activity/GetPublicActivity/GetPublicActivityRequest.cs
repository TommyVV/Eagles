using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Activity.GetPublicActivity
{
    /// <summary>
    /// 公开活动查询
    /// </summary>
    public class GetPublicActivityRequest : QueryRequestBase
    {
        /// <summary>
        /// 活动类型;0:报名;1:投票;2:问卷调查
        /// </summary>
        public ActivityType ActivityType { get; set; }
    }
}