using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.UserTest;
using Eagles.Application.Model.TestPaper.CompleteTest;
using Eagles.Application.Model.TestPaper.GetTestPaper;
using Eagles.Application.Model.TestPaper.GetIsJoinTest;

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
        public ResponseFormat<GetTestPaperResponse> GetTestPaper(GetTestPaperRequest request)
        {
            return ApiActuator.Runing(() => userTestHandler.GetTestPaper(request));
        }

        /// <summary>
        /// 用户试卷回答接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<CompleteTestResponse> TestPaperAnswer(CompleteTestRequest request)
        {
            return ApiActuator.Runing(() => userTestHandler.CompleteTest(request));
        }

        /// <summary>
        /// 查询用户是否可以回答该试卷
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetIsJoinTestResponse> GetIsJoinTest(GetIsJoinTestRequest request)
        {
            return ApiActuator.Runing(() => userTestHandler.GetIsJoinTest(request));
        }
    }
}