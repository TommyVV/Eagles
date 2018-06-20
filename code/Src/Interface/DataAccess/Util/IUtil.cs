using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.Util
{
    public interface IUtil : IInterfaceBase
    {
        UserToken GetUserId(string token, int tokenType);

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        UserInfo GetUserInfo(int userId);
    }
}