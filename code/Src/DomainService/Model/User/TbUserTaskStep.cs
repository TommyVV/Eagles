using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_TASK_STEP
    /// </summary>
    public class TbUserTaskStep
    {
        public int BranchId { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }
        public int StepId { get; set; }
        public string StepName { get; set; }
        public int TaskId { get; set; }
        public DateTime UpdateTime { get; set; }
        public int UserId { get; set; }
        public string Attach1 { get; set; }
        public string Attach2 { get; set; }
        public string Attach3 { get; set; }
        public string Attach4 { get; set; }
        public string AttachType1 { get; set; }
        public string AttachType2 { get; set; }
        public string AttachType3 { get; set; }
        public string AttachType4 { get; set; }
    }
}