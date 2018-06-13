using System;

namespace Eagles.Application.Model.Curd.Score.GetScoreExchangeLs
{
    /// <summary>
    /// 积分兑换流水查询
    /// </summary>
    public class GetScoreExchangeLsRequest : RequestBase
    {
        public string Token { get; set; }
    }
}