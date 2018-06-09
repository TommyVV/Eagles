using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.DeliverGoods
{
    public class DeliverGoodsRequset:RequestBase
    {
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }


        static readonly DateTime Dt = DateTime.Now;
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime StartTime { get; set; } = Dt;

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = Dt;

        /// <summary>
        /// 
        /// </summary>
        public DeliverStatus DeliverStatus { get; set; }
    }
}
