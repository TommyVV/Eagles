using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    /// <summary>
    /// 积分商品详情
    /// </summary>
    class IntegralGoodsDetailQueryRequest : RequestBase
    {
        public string Token { get; set; }

        public string GoodsId { get; set; }
    }
}
