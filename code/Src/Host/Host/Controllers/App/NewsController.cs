using System;
using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.News;
using Eagles.Application.Model.AppModel.News;

namespace Eagles.Application.Host.Controllers.App
{
    /// <summary>
    /// 文章Controller
    /// </summary>
    [ValidServiceToken]
    public class NewsController : ApiController
    {
        private INewsHandler newsHandler;

        public NewsController(INewsHandler newsHandler)
        {
            this.newsHandler = newsHandler;
        }

        /// <summary>
        /// 获取新闻模块
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
        /// 获取新闻详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetNewsDetail")]
        [HttpPost]
        public GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequest request)
        {
            try
            {
                return newsHandler.GetNewsDetail(request);
            }
            catch (TransactionException e)
            {
                return new GetNewsDetailResponse()
                {
                    ErrorCode = e.ErrorCode,
                    Message = e.Message
                };

            }
            catch (Exception e)
            {
                return new GetNewsDetailResponse()
                {
                    ErrorCode = "99",
                    Message = "系统错误"
                };
            }

        }
    }
}