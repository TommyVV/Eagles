using System;
using System.Web.Http;
using Eagles.Application.Model.News.GetModuleNews;
using Eagles.Application.Model.News.GetNewsDetail;
using Eagles.Application.Model.News.GetNewsTest;
using Eagles.Base;
using Eagles.Interface.Core.News;

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
        /// 获取模块内的新闻列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        /// 新闻详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取新闻试卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetNewsTestResponse GetNewsTest(GetNewsTestRequest request)
        {
            try
            {
                return newsHandler.GetNewsTest(request);
            }
            catch (TransactionException e)
            {
                return new GetNewsTestResponse()
                {
                    ErrorCode = e.ErrorCode,
                    Message = e.Message
                };

            }
            catch (Exception e)
            {
                return new GetNewsTestResponse()
                {
                    ErrorCode = "99",
                    Message = "系统错误"
                };
            }

        }
    }
}