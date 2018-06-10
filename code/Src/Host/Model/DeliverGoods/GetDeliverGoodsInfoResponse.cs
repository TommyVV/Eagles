using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.DeliverGoods
{
    public class GetDeliverGoodsInfoResponse:ResponseBase
    {
        /// </summary>
        public DeliverGoodsInfo info { get; set; }
    }
}
