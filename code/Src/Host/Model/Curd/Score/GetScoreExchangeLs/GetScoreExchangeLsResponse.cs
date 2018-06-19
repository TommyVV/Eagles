using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.Score.GetScoreExchangeLs
{
    /// <summary>
    /// 积分兑换流水查询
    /// </summary>
    public class GetScoreExchangeLsResponse : ResponseBase
    {
        /// <summary>
        /// 积分流水
        /// </summary>
        public List<ScoreExchange> ScoreList { get; set; }
    }
}