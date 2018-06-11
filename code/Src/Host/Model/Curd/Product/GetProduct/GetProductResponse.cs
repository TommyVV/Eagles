using System;

namespace Eagles.Application.Model.Curd.Product.GetProduct
{
    /// <summary>
    /// 获取积分商品
    /// </summary>
    public class GetProductResponse : ResponseBase
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品积分
        /// </summary>
        public string ProductSocre { get; set; }

        /// <summary>
        /// 产品图片Url
        /// </summary>
        public string ProductImageUrl { get; set; }
        
    }
}