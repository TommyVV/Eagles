using Eagles.Base;
using Eagles.Application.Model.Order.GetOrderLs;

namespace Eagles.Interface.Core.Order
{
    public interface IOrderHandler : IInterfaceBase
    {
        GetOrderLsResponse GetOrderLs(GetOrderLsRequest request);
    }
}