using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.AppModel.Score.GetScoreRank
{
    /// <summary>
    /// 积分排行查询
    /// </summary>
    public class GetScoreRankResponse : ResponseBase
    {
        /// <summary>
        /// 排行类型 0-全部 1-支部积分排行榜 2-支部积分排行榜
        /// </summary>
        public string RankType { get; set; }

        /// <summary>
        /// 党员积分排行榜
        /// </summary>
        public List<UserRank> UserRank { get; set; }

        /// <summary>
        /// 支部积分排行榜
        /// </summary>
        public List<BranchRank> BranchRank { get; set; }
    }
}