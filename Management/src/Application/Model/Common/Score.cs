using System;
namespace Eagles.Application.Model.Common
{
    public class ScoreExchange
    {
        /// <summary>
        /// 兑换商品名称
        /// </summary>
        public string ProdName { get; set; }

        /// <summary>
        /// 兑换积分
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// 兑换时间
        /// </summary>
        public DateTime ExchangDate { get; set; }

        /// <summary>
        /// 订单Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 商品寄送地址
        /// </summary>
        public string Address { get; set; }

    }
}