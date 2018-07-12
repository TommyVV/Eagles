using System;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.Core.UserComment;
using Eagles.Interface.DataAccess.UserComment;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.UserComment.AuditUserComment;
using Eagles.Application.Model.UserComment.EditUserComment;
using Eagles.Application.Model.UserComment.GetUserComment;
using Eagles.Application.Model.UserComment.RemoveUserComment;

namespace Eagles.DomainService.Core.UserComment
{
    public class UserCommentHandler :IUserCommentHandler
    {
        private readonly IUserCommentAccess userCommentAccess;
        private readonly IUtil util;

        public UserCommentHandler(IUserCommentAccess userCommentAccess,  IUtil util)
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
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var userId = request.CommentUserId; //评论人
            var userInfo = util.GetUserInfo(userId);
            if (userInfo == null)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var tbUserComment = new TbUserComment()
            {
                OrgId = tokens.OrgId,
                CommentType = request.CommentType,
                Id = request.Id,
                Content = request.Comment,
                CreateTime = DateTime.Now,
                UserId = userId,
                UserName = userInfo.Name,
                ReviewStatus = -1
            };
            var result = userCommentAccess.EditUserComment(tbUserComment);
            if (result <= 0)            
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);            
            return response;
        }

        public AuditUserCommentResponse AuditUserComment(AuditUserCommentRequest request)
        {
            var response = new AuditUserCommentResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var result = userCommentAccess.AuditUserComment(request.CommentId, request.ReviewStatus);
            if (result <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public RemoveUserCommentResponse RemoveUserComment(RemoveUserCommentRequest request)
        {
            var response = new RemoveUserCommentResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var result = userCommentAccess.RemoveUserComment(request.MessageId, request.Id);
            if (result <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetUserCommentResponse GetUserComment(GetUserCommentRequest request)
        {
            var response = new GetUserCommentResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var result = userCommentAccess.GetUserComment(request.CommentType, request.Id, tokens.UserId);
            response.CommentList = result?.Select(x => new Comment
            {
                CommentId = x.MessageId,
                Id = x.Id,
                CommentTime = x.ReviewTime.ToString("yyyy-MM-dd HH:mm:ss"),
                CommentUserId = x.UserId,
                CommentUserName = x.UserName,
                CommentContent = x.Content,
                CommentStatus = x.ReviewStatus
            }).ToList();
            if (result != null && result.Count > 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

    }
}