using System;

namespace Eagles.Application.Model.AppModel.Score.AppScoreExchange
{
    /// <summary>
    /// 积分兑换接口
    /// </summary>
    public class AppScoreExchangeRequest : RequestBase
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// 商品数量
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 邮寄地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
    }
}