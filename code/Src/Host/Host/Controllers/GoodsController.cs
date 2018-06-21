using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Application.Model.Goods.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodsController : ApiController
    {
        private readonly IGoodsHandler testHandler;

        public GoodsController(IGoodsHandler testHandler)
        {
            this.testHandler = testHandler;
        }
        
        /// <summary>
        /// 编辑  商品
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditGoods")]
        [HttpPost]
        public ResponseBase EditGoods(EditGoodsRequset requset)
        {
            return testHandler.EditGoods(requset);
        }

        /// <summary>
        ///  商品 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveGoods")]
        [HttpPost]
        public ResponseBase RemoveGoods(RemoveGoodsRequset requset)
        {
            return testHandler.RemoveGoods(requset);
        }

        /// <summary>
        /// 商品 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetGoods")]
        [HttpPost]
        public GetGoodsResponse GetGoods(GetGoodsRequest requset)
        {
            return testHandler.GetGoods(requset);
        }

        /// <summary>
        ///  商品 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetGoodsDetail")]
        [HttpPost]
        public GetGoodsDetailResponse GetGoodsDetail(GetGoodsDetailRequset requset)
        {
            return testHandler.GetGoodsDetail(requset);
        }

    }
}