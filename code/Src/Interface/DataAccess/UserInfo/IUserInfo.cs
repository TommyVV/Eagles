using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.Sms;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.UserInfo
{
    public interface IUserInfoAccess : IInterfaceBase
    {
        int CreateUser(TbUserInfo userInfo);

        int EditUser(TbUserInfo userInfo);

        TbUserInfo GetUserInfo(int userId);

        TbUserInfo GetLogin(string phone);

        int InsertToken(TbUserToken userToken);

        List<TbUserRelationship> GetRelationship(int userId, bool relationshipType);

        List<TbUserInfo> GetUserInfo(List<int> userIdList);

        TbValidCode GetValidCode(TbValidCode validCode);

        int InsertSmsCode(TbValidCode validateCode);
    }
}