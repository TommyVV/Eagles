using System.Web.Http;
using Eagles.Application.Model.Product.GetProduct;
using Eagles.Application.Model.Product.GetProductDetail;
using Eagles.Interface.Core.Product;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 积分商品
    /// </summary>
    [ValidServiceToken]
    public class ProductController : ApiController
    {
        private IProductHandler productHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productHandler"></param>
        public ProductController(IProductHandler productHandler)
        {
            this.productHandler = productHandler;
        }

        /// <summary>
        /// 积分商品列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetProductResponse GetProduct(GetProductRequest request)
        {
            return productHandler.GetProduct(request);
        }

        /// <summary>
        /// 积分商品明细查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetProductDetailResponse GetProductDetail(GetProductDetailRequest request)
        {
            return productHandler.GetProductDetail(request);
        }
    }
}