using System.Web.Http;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Application.Model.Goods.Response;
using Eagles.Interface.Core;
using Eagles.Application.Host.Common;


namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductController : ApiController
    {
        private readonly IGoodsHandler testHandler;

        public ProductController(IGoodsHandler testHandler)
        {
            this.testHandler = testHandler;
        }
        
        /// <summary>
        /// 新增/编辑商品
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditGoods(EditGoodsRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>testHandler.EditGoods(requset));
        }

        /// <summary>
        ///  商品删除
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveGoods(RemoveGoodsRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>testHandler.RemoveGoods(requset));
        }

        /// <summary>
        /// 商品列表查询
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetGoodsResponse> GetGoods(GetGoodsRequest requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>testHandler.GetGoods(requset));
        }

        /// <summary>
        ///  商品详情查询
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetGoodsDetailResponse> GetGoodsDetail(GetGoodsDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) =>testHandler.GetGoodsDetail(requset));
        }

    }
}