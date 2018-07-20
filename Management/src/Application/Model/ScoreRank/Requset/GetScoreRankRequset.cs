namespace Eagles.Application.Model.ScoreRank.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetScoreRankRequset : OrgListRequestBase
    {

        /// <summary>
        /// 姓名(搜索）
        /// </summary>
        public string UserName { get; set; }
    }
}
