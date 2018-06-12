using System;
using Eagles.Interface.Core.Product;
using Eagles.Application.Model.Curd.Product.GetProduct;
using Eagles.Application.Model.Curd.Product.GetProductDetail;

namespace Eagles.DomainService.Core
{
    public class ProductHandler : IProductHandler
    {
        public GetProductResponse GetProduct(GetProductRequest request)
        {
            throw new NotImplementedException();
        }

        public GetProductDetailResponse GetProductDetail(GetProductDetailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}