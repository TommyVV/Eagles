using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_ACTIVITY
    /// </summary>
    public class UserActivity
    {
        public int ActivityId { get; set; }
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string AttachType1 { get; set; }
        public string AttachType2 { get; set; }
        public string AttachType3 { get; set; }
        public string AttachType4 { get; set; }
        public int BranchId { get; set; }
        public int CompleteTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }
        public int RewardsScore { get; set; }
        public string Status { get; set; }
        public string UserFeedBack { get; set; }
        public int UserId { get; set; }
    }
}