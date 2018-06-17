using System;
using System.Linq;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.Product;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Curd.Product.GetProduct;
using Eagles.Application.Model.Curd.Product.GetProductDetail;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.Product
{
    public class ProductHandler : IProductHandler
    {
        private readonly IDbManager dbManager;
        public GetProductResponse GetProduct(GetProductRequest request)
        {
            var response = new GetProductResponse();
            var token = request.Token;
            var result = dbManager.Query<DomainModel.Product.Product>("select ProdId,ProdName,Score,ImageUrl from eagles.tb_product ", null);
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
            var productId = request.ProductId;
            var result = dbManager.Query<DomainModel.Product.Product>("select ProdId,ProdName,Score,ImageUrl,SaleCount,BeginTime,EndTime,HtmlDescription from eagles.tb_product where ProdId = @ProdId ", productId);
            if (result != null && result.Count > 0)
            {
                response.ProductId = result[0].ProdId;
                response.ProductName = result[0].ProdName;
                response.PeopleCount = result[0].SaleCount;
                response.ProductBeginTime = result[0].BeginTime;
                response.ProductEndTime = result[0].EndTime;
                response.ProductScore = result[0].Score;
                response.ProductImgUrl = result[0].ImageUrl;
                response.ProductDescrption = result[0].HtmlDescription;
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