using System.Web.Http;
using Eagles.Application.Model.Scroll.GetScrollImg;
using Eagles.Application.Model.Scroll.GetScrollNew;
using Eagles.Interface.Core.Scroll;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 滚动信息
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
        [HttpPost]
        public GetScrollNewsResponse GetScrollNews(GetScrollNewsRequest request)
        {
            return scrollHandler.GetScrollNews(request);
        }
    }
}