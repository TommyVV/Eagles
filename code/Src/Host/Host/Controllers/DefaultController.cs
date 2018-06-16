using System.Web.Http;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.Test;

namespace Eagles.Host.Controllers
{
    public class DefaultController : ApiController
    {

        private readonly IDesEncrypt desEncrypt;

        private readonly ITestHandler testHandler;

        public DefaultController(IDesEncrypt desEncrypt, ITestHandler testHandler)
        {
            this.desEncrypt = desEncrypt;
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
            var t=desEncrypt.Encrypt(str);
            var a = desEncrypt.Decrypt(t);
            var b = desEncrypt.EncryptToHex(str);
            var c = desEncrypt.DecryptToHex(b);
            testHandler.Porcess(null);
            return t;
        }
    }
}
