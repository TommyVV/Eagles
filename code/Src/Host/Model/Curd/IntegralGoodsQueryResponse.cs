using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class IntegralGoodsQueryResponse : ResponseBase
    {
        public string GoodsId { get; set; }

        public string GoodsName { get; set; }

        public string GoodsIntegral { get; set; }

        public string GoodsImage { get; set; }
        
    }
}
