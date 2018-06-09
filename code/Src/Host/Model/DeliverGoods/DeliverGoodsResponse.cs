using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.DeliverGoods
{
    public class DeliverGoodsResponse : ResponseBase
    {

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<DeliverGoodsInfo> List { get; set; }
    }
}
