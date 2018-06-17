using Eagles.Application.Model.ScoreRank.Model;

namespace Eagles.Application.Model.ScoreRank.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetScoreRankDetailResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ScoreRankInfoDetails Info { get; set; }
    }
}
