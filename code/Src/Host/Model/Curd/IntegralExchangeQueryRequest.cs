using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 积分兑换流水查询
    /// </summary>
    class IntegralExchangeQueryRequest : RequestBase
    {
        public string Token { get; set; }

        public string UserId { get; set; }
    }
}
