using System.Web.Http;
using Eagles.Application.Model.UserComment.AuditUserComment;
using Eagles.Interface.Core.UserComment;
using Eagles.Application.Model.UserComment.EditUserComment;
using Eagles.Application.Model.UserComment.GetUserComment;

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
        public GetUserCommentResponse GetUserComment(GetUserCommentRequest request)
        {
            return userCommentHandler.GetUserComment(request);
        }

        /// <summary>
        /// 用户评论接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public EditUserCommentResponse EditUserComment(EditUserCommentRequest request)
        {
            return userCommentHandler.EditUserComment(request);
        }

        /// <summary>
        /// 评论审核接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public AuditUserCommentResponse AuditUserComment(AuditUserCommentRequest request)
        {
            return userCommentHandler.AuditUserComment(request);
        }
    }
}