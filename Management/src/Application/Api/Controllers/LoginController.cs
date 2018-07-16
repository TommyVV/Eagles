using System.Web.Http;
using Eagles.Application.Host.Common;
using Eagles.Application.Model;
using Eagles.Application.Model.AuthorityGroupSetUp.Response;
using Eagles.Application.Model.Login.Model;
using Eagles.Application.Model.Login.Requset;
using Eagles.Application.Model.Login.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class LoginController : ApiController
    {
        private readonly ILoginHandler testHandler;

        private readonly IOperGroupHandler _operatorHandler;

        public LoginController(ILoginHandler testHandler, IOperGroupHandler operatorHandler)
        {
            this.testHandler = testHandler;
            this._operatorHandler = operatorHandler;
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

        /// <summary>
        /// 根据token获取当前token用户的权限 
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetAuthorityGroupSetUpResponse> GetAuthorityByToken(RequestBase requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _operatorHandler.GetAuthorityByToken(requset));
        }

    }
}