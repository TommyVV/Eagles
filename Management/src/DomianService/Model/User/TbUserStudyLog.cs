using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_STUDY_LOG
    /// </summary>
    public class TbUserStudyLog
    {
        public int BranchId { get; set; }
        public DateTime CreateTime { get; set; }
        public int ModuleId { get; set; }
        public int NewsId { get; set; }
        public int OrgId { get; set; }
        public int Score { get; set; }
        public int StudyId { get; set; }
        public int TraceId { get; set; }
        public int UserId { get; set; }
    }
}