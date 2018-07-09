using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.Order;
using Eagles.Application.Model.Order.GetOrderLs;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 订单Controller
    /// </summary>
    public class OrderController : ApiController
    {
        private IOrderHandler orderHandler;

        /// <summary>
        /// 
        /// </summary>
        public OrderController(IOrderHandler orderHandler)
        {
            this.orderHandler = orderHandler;
        }

        /// <summary>
        /// 商品积分兑换流水
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetOrderLsResponse> GetOrderLs(GetOrderLsRequest request)
        {
            return ApiActuator.Runing(() => orderHandler.GetOrderLs(request));
        }
    }
}