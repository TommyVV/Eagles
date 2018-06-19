using System;
using System.Web.Http;
using Eagles.Interface.Core.Product;
using Eagles.Application.Model.AppModel.Product.GetProduct;
using Eagles.Application.Model.AppModel.Product.GetProductDetail;

namespace Eagles.Application.Host.Controllers.App
{
    /// <summary>
    /// 积分商品Controller
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
        /// 积分商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetProduct")]
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
        [Route("api/GetProductDetail")]
        [HttpPost]
        public GetProductDetailResponse GetProductDetail(GetProductDetailRequest request)
        {
            return productHandler.GetProductDetail(request);
        }
    }
}