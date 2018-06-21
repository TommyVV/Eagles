
namespace Eagles.Application.Model.AppModel.Activity.GetActivityDetail
{
    /// <summary>
    /// 活动详情查询
    /// </summary>
    public class GetActivityDetailRequest : RequestBase
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 密文UserId
        /// </summary>
        public int EncryptUserid { get; set; }
    }
}