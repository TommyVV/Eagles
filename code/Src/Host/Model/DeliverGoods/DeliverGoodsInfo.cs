using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.DeliverGoods
{
    public class DeliverGoodsInfo
    {

        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///商品名
        /// </summary>
        public List<string> GoodsName { get; set; }

        /// <summary>
        ///下单时间
        /// </summary>
        public DateTime PlaceTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DeliverStatus DeliverStatus { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string  Remark { get; set; }
    }
}
