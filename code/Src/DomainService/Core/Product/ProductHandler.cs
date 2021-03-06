﻿using System.Linq;
using Eagles.Base;
using Eagles.Application.Model;
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
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iProductAccess.GetProduct(request);
            if (result != null && result.Count > 0)
            {
                response.ProductList = result?.Select(x => new Application.Model.Common.Product
                {
                    ProductId = x.ProdId,
                    ProductName = x.ProdName,
                    ProductScore = x.Score,
                    ProductImageUrl = x.ImageUrl
                }).ToList();
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetProductDetailResponse GetProductDetail(GetProductDetailRequest request)
        {
            var response = new GetProductDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iProductAccess.GetProductDetail(request.ProductId);
            if (result != null)
            {
                response.ProductId = result.ProdId;
                response.ProductName = result.ProdName;
                response.PeopleCount = result.SaleCount;
                response.ProductBeginTime = result.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.ProductEndTime = result.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.ProductScore = result.Score;
                response.ProductImgUrl = result.SmallImageUrl;
                response.ProductDescrption = result.HtmlDescription;
                response.Price = result.Price;
                response.Inventory = result.Stock;
                response.LimitedCount = result.MaxBuyCount;
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }
    }
}