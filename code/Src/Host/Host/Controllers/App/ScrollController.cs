using System.Web.Http;
using Eagles.Interface.Core.Scroll;
using Eagles.Application.Model.AppModel.Scroll.GetScrollImg;
using Eagles.Application.Model.AppModel.Scroll.GetScrollNew;

namespace Eagles.Application.Host.Controllers.App
{
    /// <summary>
    /// 滚动信息控制器
    /// </summary>
    [ValidServiceToken]
    public class ScrollController : ApiController
    {
        private IScrollHandler scrollHandler;

        public ScrollController(IScrollHandler scrollHandler)
        {
            this.scrollHandler = scrollHandler;
        }

        /// <summary>
        /// 滚动图片查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetScrollImg")]
        [HttpPost]
        public GetScrollImgResponse GetScrollImg(GetScrollImgRequest request)
        {
            return scrollHandler.GetScrollImg(request);
        }

        /// <summary>
        /// 滚动消息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetScrollNews")]
        [HttpPost]
        public GetScrollNewsResponse GetScrollNews(GetScrollNewsRequest request)
        {
            return scrollHandler.GetScrollNews(request);
        }
    }
}