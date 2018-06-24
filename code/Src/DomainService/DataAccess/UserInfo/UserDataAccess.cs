using System;
using System.Linq;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Sms;
using Eagles.Interface.DataAccess.UserInfo;

namespace Ealges.DomianService.DataAccess.UserInfo
{
    public class UserDataAccess :IUserInfoAccess
    {
        private readonly IDbManager dbManager;

        public UserDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int CreateUser(TbUserInfo userInfo)
        {
            var codeResult = dbManager.Query<TbValidCode>("select Phone,TbValidCode,ExpireTime FROM eagles.tb_validcode where phone = @phone ", userInfo.Phone);

            var result = dbManager.Query<TbUserInfo>("select UserId,Password from eagles.tb_user_info where Phone = @Phone ", userInfo.Phone);
            
            return dbManager.Excuted(@"insert into eagles.tb_user_info", userInfo);
        }

        public int EditUser(TbUserInfo userInfo)
        {
            return dbManager.Excuted(@"update eagles.tb_user_info set Name=@Name,Sex=@Sex,Birthday=@Birthday,Phone=@Phone,Address=@Address,Origin=@Origin,OriginAddress=@OriginAddress,Ethnic=@Ethnic,BranchId=@BranchId,
Dept=@Dept,Education=@Education,School=@School,IdNumber=@IdNumber,Company=@Company,PreMemberTime=@PreMemberTime,MemberTime=@MemberTime,MemberType=@MemberType,Provice=@Provice,City=@City,District=@District,PhotoUrl=@PhotoUrl,
IsLeader=@IsLeader where userId = @userId", userInfo);
        }

        public TbUserInfo GetUserInfo(int userId)
        {
            var userInfo = dbManager.Query<TbUserInfo>(
                @"SELECT OrgId,BranchId,UserId,Password,Name,Sex,Ethinc,Birthday,Origin,OriginAddress,Phone,IdNumber,Eduction,
School,Provice,City,District,Address,Company,Dept,Title,PreMemberTime,MemberTime,MemberType,Status,MemberStatus,
PhotoUrl,NickPhotoUrl,CreateTime,EditTime,OperId,IsCustomer FROM eagles.tb_user_info where UserId=@UserId", new { UserId = userId});
            if (userInfo != null && userInfo.Any())
                return userInfo.FirstOrDefault();
            return null;
        }

        public TbUserInfo GetLogin(int userId)
        {
            var userInfo = dbManager.Query<TbUserInfo>(
                "select UserId,Password from eagles.tb_user_info where UserId = @UserId ", new {UserId = userId});
            if (userInfo != null && userInfo.Any())
                return userInfo.FirstOrDefault();
            return null;
        }

        public string InsertToken(TbUserToken userToken)
        {
            var token = Guid.NewGuid().ToString();
            dbManager.Excuted(@"insert into eagles.tb_user_token (UserId,Token,CreateTime,ExpireTime,TokenType) value (@UserId,@Token,@CreateTime,@ExpireTime,@TokenType)",userToken);
            return token;
        }
    }
}