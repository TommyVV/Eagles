using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.Product;
using Eagles.Application.Model.Product.GetProduct;

namespace Eagles.Interface.DataAccess.ProductAccess
{
    public interface IProductAccess : IInterfaceBase
    {
        int GetOrderByProduct(int productId, int userId);

        List<TbProduct> GetProduct(GetProductRequest request );

        TbProduct GetProductDetail(int productId);
    }
}