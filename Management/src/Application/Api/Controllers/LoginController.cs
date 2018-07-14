using System.Web.Http;
using Eagles.Application.Host.Common;
using Eagles.Application.Model.Login.Model;
using Eagles.Application.Model.Login.Requset;
using Eagles.Application.Model.Login.Response;
using Eagles.Base;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : ApiController
    {
        private readonly ILoginHandler testHandler;

        public LoginController(ILoginHandler testHandler)
        {
            this.testHandler = testHandler;
        }


        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<LoginResponse> Login(LoginRequset requset)
        {          
            return ApiActuator.Runing(() => testHandler.Login(requset));
        }

        /// <summary>
        ///  获取验证码
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<string> VerificationCode(Verification requset)
        {
            return ApiActuator.Runing(() => testHandler.VerificationCode(requset));
        }

    }
}