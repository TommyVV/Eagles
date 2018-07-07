using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_TEST
    /// </summary>
    public class TbUserTest
    {
        public int BranchId { get; set; }
        public DateTime CreateTime { get; set; }
        public int OrgId { get; set; }

        /// <summary>
        /// 试卷得分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 试卷id
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// 试卷总分
        /// </summary>
        public int TotalScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int UseTime { get; set; }
    }
}