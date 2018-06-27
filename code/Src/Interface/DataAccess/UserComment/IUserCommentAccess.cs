using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.UserComment
{
    public interface IUserCommentAccess : IInterfaceBase
    {
        int EditUserComment(int orgId, int activityId, int userId, string content);

        List<TbUserComment> GetUserComment(string commentType, int id, int userId);

        int AuditUserComment(int id);
    }
}