using System;
using System.Web.Http;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.GetNewsTest;
using Eagles.Base;
using Eagles.Interface.Core.News;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// TestPaperController
    /// </summary>
    public class TestPaperController: ApiController
    {
        private INewsHandler newsHandler;

        public TestPaperController(INewsHandler newsHandler)
        {
            this.newsHandler = newsHandler;
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

        /// <summary>
        /// 用户试卷回答接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public CompleteTestResponse TestPaperAnswer(CompleteTestRequest request)
        {
            try
            {
                return newsHandler.CompleteTest(request);
            }
            catch (TransactionException e)
            {
                return new CompleteTestResponse()
                {
                    ErrorCode = e.ErrorCode,
                    Message = e.Message
                };

            }
            catch (Exception e)
            {
                return new CompleteTestResponse()
                {
                    ErrorCode = "99",
                    Message = "系统错误"
                };
            }

        }
    }
}