using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_INFO
    /// </summary>
    public class TbBranchRank
    {
        /// <summary>
        /// 排名
        /// </summary>
        public int No { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string BranchName { get; set; }
        /// <summary>
        /// 是否上级
        /// </summary>
        public int UserCount { get; set; }
        /// <summary>
        /// 用户积分
        /// </summary>
        public int Score { get; set; }
    }
}