using System;
using Eagles.Base;
using Eagles.Application.Model.AppModel.Product.GetProduct;
using Eagles.Application.Model.AppModel.Product.GetProductDetail;

namespace Eagles.Interface.Core.Product
{
    public interface IProductHandler : IInterfaceBase
    {
        /// <summary>
        /// 获取积分产品信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetProductResponse GetProduct(GetProductRequest request);

        /// <summary>
        /// 获取积分产品详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetProductDetailResponse GetProductDetail(GetProductDetailRequest request);
    }
}