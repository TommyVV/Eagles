using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_INFO
    /// </summary>
    public class TbUserRank
    {
        /// <summary>
        /// 排名
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 支部
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Score { get; set; }
    }
}