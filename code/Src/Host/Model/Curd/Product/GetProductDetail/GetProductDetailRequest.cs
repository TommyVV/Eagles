
namespace Eagles.Application.Model.Curd.Product.GetProductDetail
{
    /// <summary>
    /// 积分商品详情查询
    /// </summary>
    public class GetProductDetailRequest : RequestBase
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; }
    }
}