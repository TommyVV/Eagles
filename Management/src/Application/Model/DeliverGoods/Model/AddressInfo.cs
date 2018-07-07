using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.DeliverGoods.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AddressInfo
    {
        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 快递id
        /// </summary>
        public string ExpressId { get; set; }

        /// <summary>
        /// 收货地址
        /// </summary>
        public string Address { get; set; }
    }
}
