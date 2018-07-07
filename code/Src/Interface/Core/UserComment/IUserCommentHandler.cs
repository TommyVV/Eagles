using Eagles.Base;
using Eagles.Application.Model.UserComment.GetUserComment;
using Eagles.Application.Model.UserComment.EditUserComment;
using Eagles.Application.Model.UserComment.AuditUserComment;

namespace Eagles.Interface.Core.UserComment
{
    public interface IUserCommentHandler :IInterfaceBase
    {
        EditUserCommentResponse EditUserComment(EditUserCommentRequest request);

        GetUserCommentResponse GetUserComment(GetUserCommentRequest request);

        AuditUserCommentResponse AuditUserComment(AuditUserCommentRequest request);
    }
}
