using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class IntegralGoodsDetailQueryResponse : ResponseBase
    {
        public string GoodsId { get; set; }

        public string GoodsName { get; set; }

        public string GoodsIntegral { get; set; }

        public string GoodsImageUrl { get; set; }

        public string GoodsBuyStartDate { get; set; }

        public string GoodsBuyEndDate { get; set; }

        public string GoodsDesc { get; set; }
        
    }
}
