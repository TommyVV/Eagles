using System.Linq;
using Eagles.Interface.Core.Product;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.Core.DataBase.ProductAccess;
using Eagles.Application.Model.AppModel.Product.GetProduct;
using Eagles.Application.Model.AppModel.Product.GetProductDetail;

namespace Eagles.DomainService.Core.Product
{
    public class ProductHandler : IProductHandler
    {
        private readonly IProductAccess iProductAccess;
        private readonly IUtil util;

        public ProductHandler(IProductAccess iProductAccess, IUtil util)
        {
            this.iProductAccess = iProductAccess;
            this.util = util;
        }

        public GetProductResponse GetProduct(GetProductRequest request)
        {
            var response = new GetProductResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iProductAccess.GetProduct();
            if (result != null && result.Count > 0)
            {
                response.ProductList = result?.Select(x => new Application.Model.Common.Product
                {
                    ProductId = x.ProdId,
                    ProductName = x.ProdName,
                    ProductImageUrl = x.ImageUrl
                }).ToList();
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetProductDetailResponse GetProductDetail(GetProductDetailRequest request)
        {
            var response = new GetProductDetailResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iProductAccess.GetProductDetail(request.ProductId);
            if (result != null)
            {
                response.ProductId = result.ProdId;
                response.ProductName = result.ProdName;
                response.PeopleCount = result.SaleCount;
                response.ProductBeginTime = result.BeginTime;
                response.ProductEndTime = result.EndTime;
                response.ProductScore = result.Score;
                response.ProductImgUrl = result.ImageUrl;
                response.ProductDescrption = result.HtmlDescription;
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}