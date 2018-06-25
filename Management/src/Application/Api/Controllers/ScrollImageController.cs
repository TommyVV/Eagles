using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// ScrollImage
    /// </summary>
    public class ScrollImageController : ApiController
    {
        private readonly IScrollImageHandler _ScrollImageHandler;

        public ScrollImageController(IScrollImageHandler testHandler)
        {
            this._ScrollImageHandler = testHandler;
        }


        /// <summary>
        /// 编辑 滚动图
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditScrollImage(EditRollImageRequest requset)
        {
            return _ScrollImageHandler.EditRollImages(requset);
        }

        /// <summary>
        /// 删除 滚动图
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase RemoveScrollImage(RemoveRollImageRequset requset)
        {
            return _ScrollImageHandler.RemoveRollImages(requset);
        }

        /// <summary>
        /// 滚动图 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetRollImageDetailsResponse GetScrollImageDetail(GetRollImageDetailRequset requset)
        {
            return _ScrollImageHandler.GetRollImagesDetail(requset);
        }

        /// <summary>
        /// 滚动图 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public GetRollImageResponse GetScrollImage(GetRollImageRequest requset)
        {
            return _ScrollImageHandler.GetRollImages(requset);
        }
    }
}