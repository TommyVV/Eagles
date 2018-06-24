using Eagles.Base;

namespace Eagles.Interface.DataAccess.UserMessage
{
    public interface IUserMessageDataAccess:IInterfaceBase
    {
        int GetUserUnreadMessageCount(int userId);
    }
}
