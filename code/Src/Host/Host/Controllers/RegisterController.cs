using System.Web.Http;
using Eagles.Application.Model.Register;
using Eagles.Application.Model.User.Register;
using Eagles.Base;
using Eagles.Interface.Core.Register;
using Eagles.Interface.Core.User;

namespace Eagles.Application.Host.Controllers
{
    public class RegisterController:ApiController
    {
        private IUserHandler userHandler;

        private readonly IRegisterHandler registerHandler;

        public RegisterController(IUserHandler userHandler, IRegisterHandler registerHandler)
        {
            this.userHandler = userHandler;
            this.registerHandler = registerHandler;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<RegisterResponse> Register(RegisterRequest request)
        {
            return ApiActuator.Runing(() => userHandler.Register(request));
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<ValidateCodeResponse> GetValidateCode(ValidateCodeRequest request)
        {
            return ApiActuator.Runing(() => registerHandler.GenerateSmsCode(request));
        }
    }
}