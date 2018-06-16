using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.Util
{
    public interface IUtil:IInterfaceBase
    {
        UserToken GetUserId(string token, int tokenType);
    }
}
