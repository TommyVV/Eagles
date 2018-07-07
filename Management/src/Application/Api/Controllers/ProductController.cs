using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Application.Model.Goods.Response;
using Eagles.Base;
using Eagles.Interface.Core;

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
        /// 编辑  商品
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditGoods(EditGoodsRequset requset)
        {
            return ApiActuator.Runing(() =>testHandler.EditGoods(requset));
        }

        /// <summary>
        ///  商品 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveGoods(RemoveGoodsRequset requset)
        {
            return ApiActuator.Runing(() =>testHandler.RemoveGoods(requset));
        }

        /// <summary>
        /// 商品 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetGoodsResponse> GetGoods(GetGoodsRequest requset)
        {
            return ApiActuator.Runing(() =>testHandler.GetGoods(requset));
        }

        /// <summary>
        ///  商品 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetGoodsDetailResponse> GetGoodsDetail(GetGoodsDetailRequset requset)
        {
            return ApiActuator.Runing(() =>testHandler.GetGoodsDetail(requset));
        }

    }
}