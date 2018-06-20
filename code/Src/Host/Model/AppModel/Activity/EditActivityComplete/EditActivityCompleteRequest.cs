using System;

namespace Eagles.Application.Model.AppModel.Activity.EditActivityComplete
{
    /// <summary>
    /// 活动完成接口
    /// </summary>
    public class EditActivityCompleteRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 活动完成/审核
        /// </summary>
        public string ActivityType { get; set; }
    }
}