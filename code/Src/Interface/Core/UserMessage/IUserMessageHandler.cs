using Eagles.Application.Model.UserMessage;
using Eagles.Base;

namespace Eagles.Interface.Core.UserMessage
{
    public interface IUserMessageHandler:IInterfaceBase
    {
        GetUserUnreadMessage GetUserUnreadMessage(string token);
    }
}
