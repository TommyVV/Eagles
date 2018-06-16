using Eagles.Base;

namespace Eagles.Interface.Core.DataBase.UserInfo
{
    public interface IUserInfoAccess : IInterfaceBase
    {
        int EditUser(Application.Model.Common.UserInfo user);

        DomainService.Model.User.UserInfo GetUserInfo(int userId);

        DomainService.Model.User.UserInfo GetLogin(int userId);

        string InsertToken(int userId);
    }
}