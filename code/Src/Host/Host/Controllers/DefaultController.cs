using System.Web.Http;
using Eagles.Base.DesEncrypt;

namespace Eagles.Host.Controllers
{
    public class DefaultController : ApiController
    {

        private readonly IDesEncrypt desEncrypt;

        public DefaultController(IDesEncrypt desEncrypt)
        {
            this.desEncrypt = desEncrypt;
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
            return t;
        }
    }
}
