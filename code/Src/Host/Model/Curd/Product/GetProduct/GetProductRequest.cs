using System;

namespace Eagles.Application.Model.Curd.Product.GetProduct
{
    /// <summary>
    /// 获取积分商品
    /// </summary>
    public class GetProductRequest : RequestBase
    {
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}