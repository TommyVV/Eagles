using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.DeliverGoods.Requset;
using Eagles.Application.Model.DeliverGoods.Response;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.Organization.Response;
using Eagles.Interface.Core;

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
        public ResponseBase EditOrders(EditDeliverGoodsRequest requset)
        {
            return testHandler.EditOrders(requset);
        }


        /// <summary>
        /// 订单发货 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetDeliverGoodsDetailResponse GetOrdersDetail(GetDeliverGoodsDetailRequset requset)
        {
            return testHandler.GetOrdersDetail(requset);
        }

        /// <summary>
        /// 订单发货 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetDeliverGoodsResponse GetOrders(GetDeliverGoodsRequset requset)
        {
            return testHandler.GetOrders(requset);
        }
    }
}