using System.Collections.Generic;

namespace Eagles.Application.Model.Product.GetProduct
{
    /// <summary>
    /// 获取积分商品
    /// </summary>
    public class GetProductResponse
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<Common.Product> ProductList { get; set; }
    }
}