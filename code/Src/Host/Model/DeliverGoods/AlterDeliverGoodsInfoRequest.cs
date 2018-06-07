using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.DeliverGoods
{
    public class AlterDeliverGoodsInfoRequest
    {

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 订单主键
        /// </summary>
        public List<int> OrderId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public DeliverStatus DeliverStatus { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
