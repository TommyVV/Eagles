using System;

namespace Eagles.DomainService.Model.Activity
{
    /// <summary>
    /// TB_ACTIVITY
    /// </summary>
    public class Activity
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string ActivityType { get; set; }
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string AttachType1 { get; set; }
        public string AttachType2 { get; set; }
        public string AttachType3 { get; set; }
        public string AttachType4 { get; set; }
        public DateTime BeginTime { get; set; }
        public int BranchId { get; set; }
        public string BranchReview { get; set; }
        public int CanComment { get; set; }
        public DateTime EndTime { get; set; }
        public int FromUser { get; set; }
        public string HtmlContent { get; set; }
        public string ImageUrl { get; set; }
        public string IsPublic { get; set; }
        public int MaxCount { get; set; }
        public int MaxUser { get; set; }
        public int OrgId { get; set; }
        public string OrgReview { get; set; }
        public int TestId { get; set; }
        public int ToUserId { get; set; }
    }
}