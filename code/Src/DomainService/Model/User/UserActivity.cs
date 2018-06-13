using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_ACTIVITY
    /// </summary>
    public class UserActivity
    {
        public int ActivityId { get; set; }
        public int BranchId { get; set; }
        public int CompleteTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int orgId { get; set; }
        public int RewardsScore { get; set; }
        public string Status { get; set; }
        public string UserFeedBack { get; set; }
        public int UserId { get; set; }
    }
}