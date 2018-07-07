using System.Web.Http;
using Eagles.Application.Model.UserComment.AuditUserComment;
using Eagles.Interface.Core.UserComment;
using Eagles.Application.Model.UserComment.EditUserComment;
using Eagles.Application.Model.UserComment.GetUserComment;
using Eagles.Base;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 评论接口
    /// </summary>
    [ValidServiceToken]
    public class UserCommentController: ApiController
    {
        private IUserCommentHandler userCommentHandler;
        
        public UserCommentController(IUserCommentHandler userCommentHandler)
        {
            this.userCommentHandler = userCommentHandler;
        }

        /// <summary>
        /// 用户评论查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetUserCommentResponse> GetUserComment(GetUserCommentRequest request)
        {
            return ApiActuator.Runing(() => userCommentHandler.GetUserComment(request));
        }

        /// <summary>
        /// 用户评论接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditUserCommentResponse> EditUserComment(EditUserCommentRequest request)
        {
            return ApiActuator.Runing(() => userCommentHandler.EditUserComment(request));
        }

        /// <summary>
        /// 评论审核接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<AuditUserCommentResponse> AuditUserComment(AuditUserCommentRequest request)
        {
            return ApiActuator.Runing(() => userCommentHandler.AuditUserComment(request));
        }
    }
}