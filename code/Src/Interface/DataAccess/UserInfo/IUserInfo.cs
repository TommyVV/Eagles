using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.UserInfo
{
    public interface IUserInfoAccess : IInterfaceBase
    {
        int CreateUser(TbUserInfo userInfo);

        int EditUser(TbUserInfo userInfo);

        TbUserInfo GetUserInfo(int userId);

        TbUserInfo GetLogin(int userId);

        int InsertToken(TbUserToken userToken);

        List<TbUserRelationship> GetRelationship(int userId);
    }
}