using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class IntegralExchangeQueryResponse : ResponseBase
    {
        public List<IntegralExchange> List { get; }
    }

    class IntegralExchange
    {
        string Integral { get; set; }
        DateTime ExchangDate { get; set; }
        string ExchangeType { get; set; }
        string ExchangeUserName { get; set; }
    }
}
