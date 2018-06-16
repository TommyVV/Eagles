using System.Web.Http;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.Test;

namespace Eagles.Host.Controllers
{
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
    }
}
