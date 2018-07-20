using System.Web.Http;
using Eagles.Application.Model.DeliverGoods.Requset;
using Eagles.Application.Model.DeliverGoods.Response;
using Eagles.Interface.Core;
using Eagles.Application.Host.Common;


namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderController : ApiController
    {
        private readonly IOrderHandler testHandler;

        public OrderController(IOrderHandler testHandler)
        {
            this.testHandler = testHandler;
        }

        /// <summary>
        /// 编辑 订单发货
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditOrders(EditDeliverGoodsRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>testHandler.EditOrders(requset));
        }


        /// <summary>
        /// 订单发货 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetDeliverGoodsDetailResponse> GetOrdersDetail(GetDeliverGoodsDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>testHandler.GetOrdersDetail(requset));
        }

        /// <summary>
        /// 订单发货 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetDeliverGoodsResponse> GetOrders(GetDeliverGoodsRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>testHandler.GetOrders(requset));
        }
    }
}