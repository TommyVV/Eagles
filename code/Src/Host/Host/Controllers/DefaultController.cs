using System.Web.Http;
using Eagles.Interface.Core.Test;

namespace Eagles.Application.Host.Controllers
{
    [ValidServiceToken]
    public class DefaultController : ApiController
    {

        private readonly ITestHandler testHandler;

        public DefaultController(ITestHandler testHandler)
        {
            this.testHandler = testHandler;
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [Route("api/Test")]
        [HttpGet]
        public string GetActivity(string str)
        {
            testHandler.Porcess(null);
            return null;
        }

        [Route("api/corsTest")]
        [HttpGet]
        public Test GetTest()
        {
           return new Test()
           {
               Name = "1"
           };
        }
    }

    public class Test
    {
        public string Name { get; set; }
    }
}
