using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Goods
{
    public class GoodsInfo
    {
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public int GoodsName { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        public int Score { get; set; }

        public GoodsStatus GoodsStatus { get; set; }


    }
}
