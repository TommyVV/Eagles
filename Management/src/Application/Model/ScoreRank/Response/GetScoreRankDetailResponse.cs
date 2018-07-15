using System.Collections.Generic;
using Eagles.Application.Model.ScoreRank.Model;

namespace Eagles.Application.Model.ScoreRank.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetScoreRankDetailResponse
    {
        /// <summary>
        /// 
        /// </summary>
        public List<UserScoreTrace> Info { get; set; }
    }
}
