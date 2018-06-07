using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.DeliverGoods
{
    public class GetDeliverGoodsInfoRequest
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderId { get; set; }
    }
}
