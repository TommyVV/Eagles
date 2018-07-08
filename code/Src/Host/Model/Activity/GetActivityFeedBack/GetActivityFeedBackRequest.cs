
namespace Eagles.Application.Model.Activity.GetActivityFeedBack
{
    /// <summary>
    /// 活动反馈查询
    /// </summary>
    public class GetActivityFeedBackRequest : QueryRequestBase
    {
        /// <summary>
        /// 活动编号
        /// </summary>
        public int ActivityId { get; set; }
    }
}