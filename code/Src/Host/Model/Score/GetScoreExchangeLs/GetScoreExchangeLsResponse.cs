using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Score.GetScoreExchangeLs
{
    /// <summary>
    /// 积分兑换流水查询
    /// </summary>
    public class GetScoreExchangeLsResponse 
    {
        /// <summary>
        /// 积分流水
        /// </summary>
        public List<UserScore> ScoreList { get; set; }
    }
}