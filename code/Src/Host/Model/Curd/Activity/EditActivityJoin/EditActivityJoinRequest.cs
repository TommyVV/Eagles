using System;

namespace Eagles.Application.Model.Curd.Activity.EditActivityJoin
{
    /// <summary>
    /// 活动参与接口
    /// </summary>
    public class EditActivityJoinRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 活动Id
        /// </summary>
        public string ActivityId { get; set; }
        
    }
}