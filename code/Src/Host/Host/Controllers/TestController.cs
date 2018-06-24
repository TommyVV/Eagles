using System.Web.Http;
using Eagles.Base.Md5Helper;

namespace Eagles.Application.Host.Controllers
{
    public class TestController: ApiController
    {
        private readonly IMd5Helper md5Helper;


        public TestController(IMd5Helper md5Helper)
        {
            this.md5Helper = md5Helper;
        }

        [HttpGet]
        public string Test(string str)
        {
            return md5Helper.Md5Encypt(str);
        }
    }
}