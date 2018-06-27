namespace Eagles.Application.Model.Activity.EditActivityJoin
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
        public string JoinUserid { get; set; }
    }
}