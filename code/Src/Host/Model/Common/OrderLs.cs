using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 订单兑换流水
    /// </summary>
    public class OrderLs
    {
        /// <summary>
        /// 产品编号
        /// </summary>
        public int ProdId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProdName { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string SmallImageUrl { get; set; }

        /// <summary>
        /// 订单号
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 兑换时间
        /// </summary>
        public string CreateTime { get; set; }
    }
}