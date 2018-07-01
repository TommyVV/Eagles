using System.Collections.Generic;
using System.IO;
using System.Web;
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

        [HttpPost,HttpPut, HttpOptions]
        public string Test()
        {
            return null;
        }

        
    }
}