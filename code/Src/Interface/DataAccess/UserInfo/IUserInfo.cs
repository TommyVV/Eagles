using Eagles.Base;

namespace Eagles.Interface.DataAccess.UserInfo
{
    public interface IUserInfoAccess : IInterfaceBase
    {
        int EditUser(Application.Model.Common.UserInfo user);

        DomainService.Model.User.TbUserInfo GetUserInfo(int userId);

        DomainService.Model.User.TbUserInfo GetLogin(int userId);

        string InsertToken(int userId);
    }
}