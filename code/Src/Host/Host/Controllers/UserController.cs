using System;
using System.Web.Http;
using Eagles.Interface.Core.User;
using Eagles.Application.Model.Curd.User.Login;
using Eagles.Application.Model.Curd.User.Register;
using Eagles.Application.Model.Curd.User.EditUser;
using Eagles.Application.Model.Curd.User.GetUserInfo;

namespace Eagles.Host.Controllers
{
    /// <summary>
    /// 用户Controller
    /// </summary>
    public class UserController : ApiController
    {
        private IUserHandler userHandler;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/Login")]
        [HttpGet]
        public LoginResponse Login(LoginRequest request)
        {
            return userHandler.Login(request);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/Register")]
        [HttpGet]
        public RegisterResponse Register(RegisterRequest request)
        {
            return userHandler.Register(request);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditUser")]
        [HttpGet]
        public EditUserResponse EditUser(EditUserRequest request)
        {
            return userHandler.EditUser(request);
        }

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetUserInfo")]
        [HttpGet]
        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            return userHandler.GetUserInfo(request);
        }
    }
}