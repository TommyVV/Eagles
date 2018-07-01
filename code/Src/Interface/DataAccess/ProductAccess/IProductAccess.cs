using System.Collections.Generic;
using Eagles.Application.Model.Product.GetProduct;
using Eagles.Base;

namespace Eagles.Interface.DataAccess.ProductAccess
{
    public interface IProductAccess : IInterfaceBase
    {
        List<DomainService.Model.Product.TbProduct> GetProduct(GetProductRequest request );

        DomainService.Model.Product.TbProduct GetProductDetail(int productId);
    }
}