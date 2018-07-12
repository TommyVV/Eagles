using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.News;
using Eagles.Application.Model.News.GetModuleNews;
using Eagles.Application.Model.News.GetNewsDetail;
using Eagles.Application.Model.News.GetPublicNews;
using Eagles.Application.Model.News.AddNewsViewCount;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// NewsController
    /// </summary>
    [ValidServiceToken]
    public class NewsController : ApiController
    {
        private INewsHandler newsHandler;

        /// <summary>
        /// NewsController
        /// </summary>
        /// <param name="newsHandler"></param>
        public NewsController(INewsHandler newsHandler)
        {
            this.newsHandler = newsHandler;
        }

        /// <summary>
        /// 更新新闻阅读量
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<AddNewsViewCountResponse> AddNewsViewCount(AddNewsCountRequest request)
        {
            return ApiActuator.Runing(() => newsHandler.AddNewsViewCount(request));
        }

        /// <summary>
        /// 获取模块内的新闻列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetModuleNewsResponse> GetModuleNews(GetModuleNewsRequest request)
        {
            return ApiActuator.Runing(() => newsHandler.GetModuleNews(request));
        }

        /// <summary>
        /// 新闻详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetNewsDetailResponse> GetNewsDetail(GetNewsDetailRequest request)
        {
            return ApiActuator.Runing(() => newsHandler.GetNewsDetail(request));
        }
        
        /// <summary>
        /// 公开文章列表查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetPublicNewsResponse> GetPublicNews(GetPublicNewsRequest request)
        {
            return ApiActuator.Runing(() => newsHandler.GetPublicNews(request));
        }
    }
}