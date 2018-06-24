using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.UserMessage;

namespace Ealges.DomianService.DataAccess.UserMessage
{
    public class UserMessageDataAccess: IUserMessageDataAccess
    {
        private readonly IDbManager dbManager;

        public UserMessageDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int GetUserUnreadMessageCount(int userId)
        {
           return  dbManager.QuerySingle<int>(
                " select Count(*) from eagles.tb_user_notice where UserId=@UserId and IsRead=@IsRead",
                new {UserId = userId, IsRead = 1});
        }
    }
}
