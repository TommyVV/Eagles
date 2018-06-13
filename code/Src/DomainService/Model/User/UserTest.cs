using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_TEST
    /// </summary>
    public class UserTest
    {
        public int BranchId { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }
        public int Score { get; set; }
        public int TestId { get; set; }
        public int TotalScore { get; set; }
        public int UserId { get; set; }
        public int UseTime { get; set; }
    }
}