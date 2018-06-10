using System;
using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Curd.GetSocreExchangeLs
{
    /// <summary>
    /// 积分兑换流水查询
    /// </summary>
    public class GetSocreExchangeLsResponse : ResponseBase
    {
        /// <summary>
        /// 积分流水
        /// </summary>
        public List<SocreExchange> SocreList { get; }
    }
}