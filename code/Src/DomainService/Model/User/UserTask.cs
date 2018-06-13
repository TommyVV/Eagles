using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_TASK
    /// </summary>
    public class UserTask
    {
        public DateTime AcceptId { get; set; }
        public int BranchId { get; set; }
        public string Comment { get; set; }
        public int OrgId { get; set; }
        public int RewardsScore { get; set; }
        public int Score { get; set; }
        public int Status { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}