using System;

namespace Eagles.DomainService.Model.RewardScore
{
    /// <summary>
    /// TB_REWARD_SCORE
    /// </summary>
    public class TbRewardScore
    {
        public int BranchId { get; set; }
        public string keyWord { get; set; }
        public int LearnTime { get; set; }
        public int OrgId { get; set; }
        public int RewardId { get; set; }
        public int RewardType { get; set; }
        public int Score { get; set; }
        public int WordCount { get; set; }
    }
}