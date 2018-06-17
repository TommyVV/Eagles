using System;
using System.Web.Http;
using Eagles.Application.Model.Curd.News;
using Eagles.Application.Model.Curd.News.CreateNews;
using Eagles.Application.Model.Curd.News.GetNews;
using Eagles.Base;
using Eagles.Interface.Core.News;

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
        /// 获取模块文章
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetModuleNews")]
        [HttpPost]
        public GetModuleNewsResponse GetModuleNews(GetModuleNewsRequest request)
        {
            try
            {
                return newsHandler.GetModuleNews(request);
            }
            catch (TransactionException e)
            {
                return new GetModuleNewsResponse()
                {
                    ErrorCode = e.ErrorCode,
                    Message = e.Message
                };
                
            }
            catch (Exception e)
            {
                return new GetModuleNewsResponse()
                {
                    ErrorCode = "99",
                    Message = "系统错误"
                };
            }
            
        }

        /// <summary>
        /// 获取模块文章
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetNewsDetail")]
        [HttpPost]
        public GetModuleNewsResponse GetNewsDetail(GetNewsDetailRequest request)
        {
            try
            {
                return newsHandler.GetModuleNews(request);
            }
            catch (TransactionException e)
            {
                return new GetModuleNewsResponse()
                {
                    ErrorCode = e.ErrorCode,
                    Message = e.Message
                };

            }
            catch (Exception e)
            {
                return new GetModuleNewsResponse()
                {
                    ErrorCode = "99",
                    Message = "系统错误"
                };
            }

        }
    }
}