using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Activity.EditActivityReview
{
    /// <summary>
    /// 活动参与接口
    /// </summary>
    public class EditActivityReviewRequest : RequestBase
    {
        /// <summary>
        /// 01-上级审核任务 02-下级接受任务 03-下级申请完成
        /// </summary>
        public ActivityTypeEnum Type { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }
        
    }
}