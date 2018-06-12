using System;
using System.Web.Http;
using Eagles.Interface.Core.Product;
using Eagles.Application.Model.Curd.Product.GetProduct;
using Eagles.Application.Model.Curd.Product.GetProductDetail;

namespace Eagles.Host.Controllers
{
    /// <summary>
    /// 积分商品Controller
    /// </summary>
    public class ProductController : ApiController
    {
        private IProductHandler productHandler;

        /// <summary>
        /// 积分商品查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetProduct")]
        [HttpGet]
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
        [HttpGet]
        public GetProductDetailResponse GetProductDetail(GetProductDetailRequest request)
        {
            return productHandler.GetProductDetail(request);
        }
    }
}