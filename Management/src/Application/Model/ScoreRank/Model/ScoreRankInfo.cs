using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreRank.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ScoreRankInfo
    {

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public int Score { get; set; }
        ///// <summary>
        ///// 用户使用积分
        ///// </summary>
        //public string UseScore { get; set; }

        /// <summary>
        /// 用户身份
        /// </summary>
        public int UserIdentity { get; set; }

    }
}
