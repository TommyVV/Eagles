
namespace Eagles.Application.Model.Activity.GetPublicActivityDetail
{
    /// <summary>
    /// 公开活动详情查询
    /// </summary>
    public class GetPublicActivityDetailRequest : RequestBase
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }
        
    }
}