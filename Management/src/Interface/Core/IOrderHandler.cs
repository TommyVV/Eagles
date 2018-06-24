using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.DeliverGoods.Requset;
using Eagles.Application.Model.DeliverGoods.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IOrderHandler: IInterfaceBase
    {
          /// <summary>
        /// 编辑 订单发货
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase EditOrders(EditDeliverGoodsRequest requset);

        /// <summary>
        /// 订单发货 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetDeliverGoodsDetailResponse GetOrdersDetail(GetDeliverGoodsDetailRequset requset);

        /// <summary>
        /// 订单发货 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetDeliverGoodsResponse GetOrders(GetDeliverGoodsRequset requset);
    }
}
