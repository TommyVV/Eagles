using System.Linq;
using Eagles.Base;
using Eagles.Interface.Core.Product;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.ProductAccess;
using Eagles.Application.Model.Product.GetProduct;
using Eagles.Application.Model.Product.GetProductDetail;

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
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = iProductAccess.GetProduct();
            if (result != null && result.Count > 0)
            {
                response.ProductList = result?.Select(x => new Application.Model.Common.Product
                {
                    ProductId = x.ProdId,
                    ProductName = x.ProdName,
                    ProductImageUrl = x.ImageUrl
                }).ToList();
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetProductDetailResponse GetProductDetail(GetProductDetailRequest request)
        {
            var response = new GetProductDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
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
                response.Price = result.Price;
                response.Inventory = result.Stock;
                response.LimitedCount = result.MaxBuyCount;
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}