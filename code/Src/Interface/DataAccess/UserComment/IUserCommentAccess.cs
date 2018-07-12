using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.UserComment
{
    public interface IUserCommentAccess : IInterfaceBase
    {
        int EditUserComment(TbUserComment userComment);

        int RemoveUserComment(int messageId, int id);

        int AuditUserComment(int commentId, int reviewStatus);

        List<TbUserComment> GetUserComment(string commentType, int id, int userId, int commentStatus);

    }
}