
namespace Eagles.Application.Model.Product.GetProduct
{
    /// <summary>
    /// 获取积分商品
    /// </summary>
    public class GetProductRequest
    {
        /// <summary>
        /// AppId
        /// </summary>
        public int AppId { get; set; }
        /// <summary>
        ///积分商品名称
        /// </summary>
        public string ProductName { get; set; }
    }
}