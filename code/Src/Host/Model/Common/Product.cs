using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 积分商品
    /// </summary>
    public class Product
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品积分
        /// </summary>
        public string ProductScore { get; set; }

        /// <summary>
        /// 产品图片Url
        /// </summary>
        public string ProductImageUrl { get; set; }
    }
}