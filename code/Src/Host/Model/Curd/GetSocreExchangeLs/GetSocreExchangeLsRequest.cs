using System;

namespace Eagles.Application.Model.Curd.GetSocreExchangeLs
{
    /// <summary>
    /// 积分兑换流水查询
    /// </summary>
    class GetSocreExchangeLsRequest : RequestBase
    {
        public string Token { get; set; }
    }
}