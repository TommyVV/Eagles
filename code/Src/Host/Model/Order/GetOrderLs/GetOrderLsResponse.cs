using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Order.GetOrderLs
{
    /// <summary>
    /// 商品积分兑换流水
    /// </summary>
    public class GetOrderLsResponse
    {
        /// <summary>
        /// 商品积分兑换流水
        /// </summary>
        public List<OrderLs> OrderList { get; set; }
    }
}