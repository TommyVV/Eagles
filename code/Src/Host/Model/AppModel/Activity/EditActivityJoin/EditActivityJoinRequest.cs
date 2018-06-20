using System;

namespace Eagles.Application.Model.AppModel.Activity.EditActivityJoin
{
    /// <summary>
    /// 活动参与接口
    /// </summary>
    public class EditActivityJoinRequest : RequestBase
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 参与活动UserId
        /// </summary>
        public int JoinUserid { get; set; }
    }
}