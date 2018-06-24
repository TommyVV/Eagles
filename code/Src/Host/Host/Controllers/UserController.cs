using System.Web.Http;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.Register;
using Eagles.Interface.Core.News;
using Eagles.Interface.Core.User;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 用户Controller
    /// </summary>
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
        public LoginResponse Login(LoginRequest request)
        {
            return userHandler.Login(request);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public RegisterResponse Register(RegisterRequest request)
        {
            return userHandler.Register(request);
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public EditUserResponse EditUser(EditUserRequest request)
        {
            return userHandler.EditUser(request);
        }

        /// <summary>
        /// 用户信息查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetUserInfoResponse GetUserInfo(GetUserInfoRequest request)
        {
            return userHandler.GetUserInfo(request);
        }

        /// <summary>
        /// 文章发布
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public CreateNewsResponse CreateNews(CreateNewsRequest request)
        {
            return newsHandler.CreateNews(request);
        }

        /// <summary>
        /// 我的文章列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetNewsResponse GetUserArticle(GetNewsRequest request)
        {
            return newsHandler.GetUserArticle(request);
        }
    }
}