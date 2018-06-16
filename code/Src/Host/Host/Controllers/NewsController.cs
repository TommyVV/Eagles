using System;
using System.Web.Http;
using Eagles.Interface.Core.News;
using Eagles.Application.Model.Curd.News.CreateNews;
using Eagles.Application.Model.Curd.News.GetNews;

namespace Eagles.Host.Controllers
{
    /// <summary>
    /// 文章Controller
    /// </summary>
    public class NewsController : ApiController
    {
        private INewsHandler newsHandler;

        public NewsController(INewsHandler newsHandler)
        {
            this.newsHandler = newsHandler;
        }

        /// <summary>
        /// 文章发布
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/CreateNews")]
        [HttpGet]
        public CreateNewsResponse CreateNews(CreateNewsRequest request)
        {
            return newsHandler.CreateNews(request);
        }

        /// <summary>
        /// 我的文章列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetUserArticle")]
        [HttpPost]
        public GetNewsResponse GetUserArticle(GetNewsRequest request)
        {
            return newsHandler.GetUserArticle(request);
        }
    }
}