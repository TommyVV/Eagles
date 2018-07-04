using System.Collections.Generic;
using Eagles.Application.Model.ScoreRank.Model;

namespace Eagles.Application.Model.ScoreRank.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetScoreRankResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<ScoreRankInfo> List { get; set; }
    }
}
