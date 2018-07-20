using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.User;
using Eagles.Interface.Core.News;
using Eagles.Application.Model.News.CreateNews;
using Eagles.Application.Model.User.Login;
using Eagles.Application.Model.User.EditUser;
using Eagles.Application.Model.User.BranchUser;
using Eagles.Application.Model.User.EditUserPwd;
using Eagles.Application.Model.User.EditUserNoticeIsRead;
using Eagles.Application.Model.News.GetNews;
using Eagles.Application.Model.User.GetUserInfo;
using Eagles.Application.Model.User.GetUserNotice;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userHandler"></param>
        /// <param name="newsHandler"></param>
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
        /// 用户密码修改
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditUserPwdResponse> EditUserPwd(EditUserPwdRequest request)
        {
            return ApiActuator.Runing(() => userHandler.EditUserPwd(request));
        }

        /// <summary>
        /// 更新用户通知为已读
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditUserNoticeIsReadResponse> EditUserNewsIsRead(EditUserNoticeIsReadRequest request)
        {
            return ApiActuator.Runing(() => userHandler.EditUserNoticeIsRead(request));
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
        /// 用户通知查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetUserNoticeResponse> GetUserNotice(GetUserNoticeRequest request)
        {
            return ApiActuator.Runing(() => userHandler.GetUserNotice(request));
        }

        /// <summary>
        /// 用户上下级查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetUserRelationshipResponse> GetUserRelationship(GetUserRelationshipRequest request)
        {
            return ApiActuator.Runing(() => userHandler.GetUserRelationship(request)); 
        }

        /// <summary>
        /// 支部党员查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetBranchUserResponse> GetBranchUser(GetBranchUserRequest request)
        {
            return ApiActuator.Runing(() => userHandler.GetBranchUser(request));
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
    }
}