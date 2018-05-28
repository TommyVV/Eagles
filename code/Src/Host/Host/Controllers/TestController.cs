using System;
using System.Web.Http;
using Eagles.Application.Model.Test;
using Eagles.Base;
using Eagles.Base.Logger;
using Eagles.Interface.Core.Test;

namespace Eagles.Host.Controllers
{
    public class TestController : ApiController
    {
        private ITestHandler testHandler;

        private ILogger log;

        public TestController(ITestHandler testHandler, ILogger log)
        {
            this.testHandler = testHandler;
            this.log = log;
        }

        /// <summary>
        /// Demo
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/test")]
        [HttpPost]
        public TestResponse Demo(TestRequest request)
        {
            log.LoggerInfo("hahaha");
            throw new TransactionException("nihao","haha");
           return new TestResponse()
           {
               ErrorCode = "00",
               Message = "hha"
           };
            return testHandler.Porcess(request);
        }
    }
}
