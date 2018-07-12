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

        int EditUserNoticeIsRead(int newsId);

        TbUserInfo GetUserInfo(int userId);

        TbUserInfo GetLogin(string phone);

        int InsertToken(TbUserToken userToken);

        List<TbUserRelationship> GetRelationship(int userId, bool relationshipType);

        List<TbUserInfo> GetUserInfo(List<int> userIdList);

        List<TbUserNotice> GetUserNotice(int userId, int appId, int pageIndex = 1, int pageSize = 10);

        TbValidCode GetValidCode(TbValidCode validCode);

        int InsertSmsCode(TbValidCode validateCode);

        List<TbUserInfo> GetBranchUser(int branchId);
    }
}