using System.Text;
using System.Linq;
using System.Collections.Generic;
using Dapper;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Sms;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.UserInfo;

namespace Ealges.DomianService.DataAccess.UserInfo
{
    public class UserDataAccess : IUserInfoAccess
    {
        private readonly IDbManager dbManager;

        public UserDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int InsertToken(TbUserToken userToken)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_token (UserId,Token,CreateTime,ExpireTime,TokenType) value (@UserId,@Token,@CreateTime,@ExpireTime,@TokenType)", userToken);
        }

        public int CreateUser(TbUserInfo userInfo)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_info (Phone,Password,CreateTime) value (@Phone,@Password,@CreateTime)", userInfo);
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

        public TbUserInfo GetLogin(string phone)
        {
            var userInfo = dbManager.Query<TbUserInfo>("select UserId,Password from eagles.tb_user_info where Phone = @Phone ", new { Phone = phone });
            if (userInfo != null && userInfo.Any())
                return userInfo.FirstOrDefault();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="relationshipType">ture 为查询上级信息  false 为查询下级信息</param>
        /// <returns></returns>
        public List<TbUserRelationship> GetRelationship(int userId,bool relationshipType)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            if (relationshipType)
            {
                parameter.Append(" and UserId = @UserId ");
                dynamicParams.Add("UserId", userId);
            }
            else           
            {
                parameter.Append(" and SubUserId = @SubUserId ");
                dynamicParams.Add("SubUserId", userId);
            }
            sql.AppendFormat(@" select UserId,SubUserId from eagles.tb_user_relationship where  1=1  {0} ", parameter);
            return dbManager.Query<TbUserRelationship>(sql.ToString(), dynamicParams);
        }

        public List<TbUserInfo> GetUserInfo(List<int> userIdList)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            sql.Append(@"    SELECT `tb_user_info`.`OrgId`,
    `tb_user_info`.`BranchId`,
    `tb_user_info`.`UserId`,
    `tb_user_info`.`Password`,
    `tb_user_info`.`Name`,
    `tb_user_info`.`Sex`,
    `tb_user_info`.`Ethinc`,
    `tb_user_info`.`Birthday`,
    `tb_user_info`.`Origin`,
    `tb_user_info`.`OriginAddress`,
    `tb_user_info`.`Phone`,
    `tb_user_info`.`IdNumber`,
    `tb_user_info`.`Eduction`,
    `tb_user_info`.`School`,
    `tb_user_info`.`Provice`,
    `tb_user_info`.`City`,
    `tb_user_info`.`District`,
    `tb_user_info`.`Address`,
    `tb_user_info`.`Company`,
    `tb_user_info`.`Dept`,
    `tb_user_info`.`Title`,
    `tb_user_info`.`PreMemberTime`,
    `tb_user_info`.`MemberTime`,
    `tb_user_info`.`MemberType`,
    `tb_user_info`.`Status`,
    `tb_user_info`.`MemberStatus`,
    `tb_user_info`.`PhotoUrl`,
    `tb_user_info`.`NickPhotoUrl`,
    `tb_user_info`.`CreateTime`,
    `tb_user_info`.`EditTime`,
    `tb_user_info`.`OperId`,
    `tb_user_info`.`IsCustomer`,
    `tb_user_info`.`Score`,
    `tb_user_info`.`IsLeader`
FROM `eagles`.`tb_user_info`  where  UserId  in @UserId
 ");
            dynamicParams.Add("UserId", userIdList.ToArray() );

            return dbManager.Query<TbUserInfo>(sql.ToString(), dynamicParams);
        }
        
        public TbValidCode GetValidCode(TbValidCode validCode)
        {
            return dbManager.QuerySingle<TbValidCode>("select Phone from eagles.tb_validcode where Phone = @Phone and ValidCode = @ValidCode and Seq = @Seq ", validCode);
        }
    }
}