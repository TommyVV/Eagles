using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Goods
{
    public class GoodsRequest:RequestBase
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        public GoodsStatus GoodsStatus { get; set; }
    }
}
