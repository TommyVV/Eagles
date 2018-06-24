using System.Linq;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.Core.UserComment;
using Eagles.Interface.DataAccess.UserComment;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.UserComment.AuditUserComment;
using Eagles.Application.Model.UserComment.EditUserComment;
using Eagles.Application.Model.UserComment.GetUserComment;

namespace Eagles.DomainService.Core.UserComment
{
    public class UserCommentHandler :IUserCommentHandler
    {
        private readonly IUserCommentAccess userCommentAccess;
        private readonly IUtil util;

        public UserCommentHandler(IUserCommentAccess userCommentAccess, IUtil util)
        {
            this.userCommentAccess = userCommentAccess;
            this.util = util;
        }

        public EditUserCommentResponse EditUserComment(EditUserCommentRequest request)
        {
            var response = new EditUserCommentResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = userCommentAccess.EditUserComment(tokens.OrgId, request.Id, request.CommentUserId, request.Comment);
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "失败";
            }
            return response;
        }

        public GetUserCommentResponse GetUserComment(GetUserCommentRequest request)
        {
            var response = new GetUserCommentResponse();
            var result = userCommentAccess.GetUserComment(request.Id);
            response.CommentList = result?.Select(x => new Comment
            {
                CommentId = x.MessageId,
                CommentTime = x.ReviewTime,
                CommentUserId = x.UserId,
                CommentContent = x.Content
            }).ToList();
            if (result != null && result.Count > 0)
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public AuditUserCommentResponse AuditUserComment(AuditUserCommentRequest request)
        {
            var response = new AuditUserCommentResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = userCommentAccess.AuditUserComment(request.Id);
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "审核成功";
            }
            else
            {
                response.ErrorCode = "00";
                response.Message = "审核失败";
            }
            return response;
        }
    }
}