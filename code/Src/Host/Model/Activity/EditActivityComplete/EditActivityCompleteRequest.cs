namespace Eagles.Application.Model.Activity.EditActivityComplete
{
    /// <summary>
    /// 活动完成接口
    /// </summary>
    public class EditActivityCompleteRequest : RequestBase
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; }

        /// <summary>
        /// 活动通过/拒绝
        /// </summary>
        public int CompleteStatus { get; set; }
    }
}