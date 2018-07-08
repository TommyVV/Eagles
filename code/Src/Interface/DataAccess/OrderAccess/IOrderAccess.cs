using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.Order;

namespace Eagles.Interface.DataAccess.OrderAccess
{
    public interface IOrderAccess : IInterfaceBase
    {
        List<TbOrder> GetOrderLs(int userId, int pageIndex = 1, int pageSize = 10);
    }
}