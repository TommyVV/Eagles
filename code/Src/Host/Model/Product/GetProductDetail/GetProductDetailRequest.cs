
namespace Eagles.Application.Model.Product.GetProductDetail
{
    /// <summary>
    /// 积分商品详情查询
    /// </summary>
    public class GetProductDetailRequest 
    {
        /// <summary>
        /// AppId
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; }
    }
}