using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Application.Model.RollImage.Response;
using Eagles.Base;
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
        public ResponseFormat<bool> EditScrollImage(EditRollImageRequest requset)
        {
            return ApiActuator.Runing(() =>_ScrollImageHandler.EditRollImages(requset));
        }

        /// <summary>
        /// 删除 滚动图
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveScrollImage(RemoveRollImageRequset requset)
        {
            return ApiActuator.Runing(() =>_ScrollImageHandler.RemoveRollImages(requset));
        }

        /// <summary>
        /// 滚动图 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetRollImageDetailsResponse> GetScrollImageDetail(GetRollImageDetailRequset requset)
        {
            return ApiActuator.Runing(() =>_ScrollImageHandler.GetRollImagesDetail(requset));
        }

        /// <summary>
        /// 滚动图 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetRollImageResponse> GetScrollImage(GetRollImageRequest requset)
        {
            return ApiActuator.Runing(() =>_ScrollImageHandler.GetRollImages(requset));
        }
    }
}