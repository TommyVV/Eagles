using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.DeliverGoods.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.Order;

namespace Eagles.Interface.DataAccess
{
    public interface IOrderDataAccess : IInterfaceBase
    {
        List<TbOrder> GetOrderList(GetDeliverGoodsRequset requset);
        TbOrder GetOrderDetail( GetDeliverGoodsDetailRequset requset);
        int EditOrder(List<TbOrder> mod);
    }
}
