using System.Collections.Generic;
using Eagles.Base;

namespace Eagles.Interface.Core.DataBase.ProductAccess
{
    public interface IProductAccess : IInterfaceBase
    {
        List<DomainService.Model.Product.Product> GetProduct();

        DomainService.Model.Product.Product GetProductDetail(int productId);
    }
}