using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.News;
using Eagles.Interface.Core.User;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.Register;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.GetUserRelationship;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 用户Controller
    /// </summary>
    [ValidServiceToken]
    public class UserController : ApiController
    {
        private IUserHandler userHandler;
        private INewsHandler newsHandler;

        public UserController(IUserHandler userHandler, INewsHandler newsHandler)
        {
            this.userHandler = userHandler;
            this.newsHandler = newsHandler;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<LoginResponse> Login(LoginRequest request)
        {
            return ApiActuator.Runing(() => userHandler.Login(request));
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ResponseFormat<RegisterResponse> Register(RegisterRequest request)
        {
            return ApiActuator.Runing(() => userHandler.Register(request));
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditUserResponse> EditUser(EditUserRequest request)
        {
            return ApiActuator.Runing(() => userHandler.EditUser(request));
        }

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetUserInfoResponse> GetUserInfo(GetUserInfoRequest request)
        {
            return ApiActuator.Runing(() => userHandler.GetUserInfo(request));
        }

        /// <summary>
        /// 用户文章发布
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<CreateArticleResponse> CreateArticle(CreateArticleRequest request)
        {
            return ApiActuator.Runing(() => newsHandler.CreateArticle(request));
        }

        /// <summary>
        /// 我的文章列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetNewsResponse> GetUserArticle(GetNewsRequest request)
        {
            return ApiActuator.Runing(() => newsHandler.GetUserArticle(request));
        }

        /// <summary>
        /// 用户上下级查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetUserRelationshipResponse GetUserRelationship(GetUserRelationshipRequest request)
        {
            return userHandler.GetUserRelationship(request);
        }
    }
}