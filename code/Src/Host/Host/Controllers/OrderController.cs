using System.Web.Http;
using Eagles.Application.Model.Order.GetOrderLs;
using Eagles.Base;
using Eagles.Interface.Core.Order;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 订单Controller
    /// </summary>
    public class OrderController : ApiController
    {
        private readonly IOrderHandler orderHandler;

        /// <summary>
        /// 
        /// </summary>
        public OrderController(IOrderHandler orderHandler)
        {
            this.orderHandler = orderHandler;
        }

        [HttpPost]
        public ResponseFormat<GetOrderLsResponse> GetUserOrder(GetOrderLsRequest request)
        {
            return ApiActuator.Runing(() => orderHandler.GetOrderLs(request));
        }
    }
}