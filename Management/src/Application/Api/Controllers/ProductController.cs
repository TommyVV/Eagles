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
        public ResponseBase EditGoods(EditGoodsRequset requset)
        {
            return testHandler.EditGoods(requset);
        }

        /// <summary>
        ///  商品 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
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
        [HttpPost]
        public GetGoodsDetailResponse GetGoodsDetail(GetGoodsDetailRequset requset)
        {
            return testHandler.GetGoodsDetail(requset);
        }

    }
}