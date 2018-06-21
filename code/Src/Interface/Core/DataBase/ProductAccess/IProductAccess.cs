﻿using System.Collections.Generic;
using Eagles.Base;

namespace Eagles.Interface.Core.DataBase.ProductAccess
{
    public interface IProductAccess : IInterfaceBase
    {
        List<DomainService.Model.Product.TbProduct> GetProduct();

        DomainService.Model.Product.TbProduct GetProductDetail(int productId);
    }
}