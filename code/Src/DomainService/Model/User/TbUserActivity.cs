using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_ACTIVITY
    /// </summary>
    public class TbUserActivity
    {
        public int ActivityId { get; set; }
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string AttachName1 { get; set; }
        public string AttachName2 { get; set; }
        public string AttachName3 { get; set; }
        public string AttachName4 { get; set; }
        public int BranchId { get; set; }
        public int CompleteTime { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }
        public int RewardsScore { get; set; }
        public string Status { get; set; }
        public string UserFeedBack { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}