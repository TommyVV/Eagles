using System;
using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.UserTest;
using Eagles.Application.Model.News.CompleteTest;
using Eagles.Application.Model.News.GetTestPaper;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// TestPaperController
    /// </summary>
    [ValidServiceToken]
    public class TestPaperController: ApiController
    {
        private IUserTestHandler userTestHandler;

        public TestPaperController(IUserTestHandler userTestHandler)
        {
            this.userTestHandler = userTestHandler;
        }
        
        /// <summary>
        /// 获取新闻试卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetTestPaperResponse GetTestPaper(GetTestPaperRequest request)
        {
            try
            {
                return userTestHandler.GetTestPaper(request);
            }
            catch (TransactionException e)
            {
                return new GetTestPaperResponse()
                {
                    Code = e.ErrorCode,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                return new GetTestPaperResponse()
                {
                    Code = "99",
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
                return userTestHandler.CompleteTest(request);
            }
            catch (TransactionException e)
            {
                return new CompleteTestResponse()
                {
                    Code = e.ErrorCode,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {
                return new CompleteTestResponse()
                {
                    Code = "99",
                    Message = "系统错误"
                };
            }

        }
    }
}