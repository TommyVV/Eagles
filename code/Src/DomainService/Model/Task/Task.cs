using System;

namespace Eagles.DomainService.Model.Task
{
    /// <summary>
    /// TB_TASK
    /// </summary>
    public class Task
    {
        public string Attch1 { get; set; }
        public string Attch2 { get; set; }
        public string Attch3 { get; set; }
        public string Attch4 { get; set; }
        public DateTime BeginTime { get; set; }
        public int BranchId { get; set; }
        public string BranchReview { get; set; }
        public int CanComment { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime endTime { get; set; }
        public int FromUser { get; set; }
        public int IsPublic { get; set; }
        public int OrgId { get; set; }
        public string OrgReview { get; set; }
        public int Status { get; set; }
        public string TaskContent { get; set; }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
    }
}