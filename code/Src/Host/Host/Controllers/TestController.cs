using System.Web.Http;
using Eagles.Base.Md5Helper;
using Eagles.Base.ValidateVode;

namespace Eagles.Application.Host.Controllers
{
    public class TestController: ApiController
    {
        private readonly IMd5Helper md5Helper;

        private readonly IValidateCode validate;

        public TestController(IMd5Helper md5Helper, IValidateCode validate)
        {
            this.md5Helper = md5Helper;
            this.validate = validate;
        }

        [HttpGet]
        public string Test(string str)
        {
            return validate.GenerateValidCodeToBase64(1234);
        }
    }
}