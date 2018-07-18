using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;
using Eagles.Application.Model.PartyMember.Requset;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class PartyMemberDataAccess : IPartyMemberDataAccess
    {
        private readonly IDbManager dbManager;

        public PartyMemberDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }


        public TbUserInfo GetUserInfoDetail(GetUserInfoDetailRequest requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@"     
SELECT `tb_user_info`.`OrgId`,
    `tb_user_info`.`BranchId`,
    `tb_user_info`.`UserId`,
    `tb_user_info`.`Password`,
    `tb_user_info`.`Name`,
    `tb_user_info`.`Sex`,
    `tb_user_info`.`Birthday`,
    `tb_user_info`.`Origin`,
    `tb_user_info`.`OriginAddress`,
    `tb_user_info`.`Phone`,
    `tb_user_info`.`IdNumber`,
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
FROM `eagles`.`tb_user_info`
  where UserId=@UserId;
 ");
            dynamicParams.Add("UserId", requset.UserId);

            return dbManager.QuerySingle<TbUserInfo>(sql.ToString(), dynamicParams);
        }

        public int RemoveUserInfo(RemoveUserInfoDetailsRequest requset)
        {
            return dbManager.Excuted(@"  DELETE FROM `eagles`.`tb_user_info`  where
                `UserId` = @UserId;
", new { requset.UserId });
        }

        public int EditUserInfo(TbUserInfo info)
        {
            return dbManager.Excuted(@"
UPDATE `eagles`.`tb_user_info`
SET
`OrgId` = @OrgId,
`BranchId` = @BranchId,
`Name` = @Name,
`Sex` = @Sex,
`Ethnic` = @Ethnic,
`Birthday` = @Birthday,
`Origin` = @Origin,
`OriginAddress` = @OriginAddress,
`Phone` = @Phone,
`IdNumber` = @IdNumber,
`Education` = @Education,
`School` = @School,
`Provice` = @Provice,
`City` = @City,
`District` = @District,
`Address` = @Address,
`Company` = @Company,
`Dept` = @Dept,
`Title` = @Title,
`PreMemberTime` = @PreMemberTime,
`MemberTime` = @MemberTime,
`MemberType` = @MemberType,
`Status` = @Status,
`MemberStatus` = @MemberStatus,
`PhotoUrl` = @PhotoUrl,
`NickPhotoUrl` = @NickPhotoUrl,
`EditTime` = now(),
`OperId` = @OperId,
`IsCustomer` = @IsCustomer,
`Score` = @Score,
`IsLeader` = @IsLeader
WHERE 
`UserId` = @UserId

", info);

        }

        public int CreateUserInfo(TbUserInfo info)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_user_info`
(`OrgId`,
`BranchId`,
`UserId`,
`Password`,
`Name`,
`Sex`,
`Ethnic`,
`Birthday`,
`Origin`,
`OriginAddress`,
`Phone`,
`IdNumber`,
`Education`,
`School`,
`Provice`,
`City`,
`District`,
`Address`,
`Company`,
`Dept`,
`Title`,
`PreMemberTime`,
`MemberTime`,
`MemberType`,
`Status`,
`MemberStatus`,
`PhotoUrl`,
`NickPhotoUrl`,
`CreateTime`,
`EditTime`,
`OperId`,
`IsCustomer`,
`Score`,
`IsLeader`)
VALUES
(@OrgId,
@BranchId,
@UserId,
@Password,
@Name,
@Sex,
@Ethnic,
@Birthday,
@Origin,
@OriginAddress,
@Phone,
@IdNumber,
@Education,
@School,
@Provice,
@City,
@District,
@Address,
@Company,
@Dept,
@Title,
@PreMemberTime,
@MemberTime,
@MemberType,
@Status,
@MemberStatus,
@PhotoUrl,
@NickPhotoUrl,
now(),
now(),
@OperId,
@IsCustomer,
@Score,
@IsLeader);



", info);
        }

        public List<TbUserInfo> GetUserInfoList(GetPartyMemberRequest request, out int totalCount)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            //if (request.UserIdentity>0)
            //{
            //    parameter.Append(" and MemberTime = @UserIdentity ");
            //    dynamicParams.Add("UserIdentity", (int)request.UserIdentity);
            //}

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                parameter.Append(" and Name = @Name ");
                dynamicParams.Add("Name", request.UserName);
            }

            //if (request.BranchId>0)
            //{
            //    parameter.Append(" and BranchId = @BranchId ");
            //    dynamicParams.Add("BranchId", request.BranchId);
            //}

            //if (request.OrgId > 0)
            //{
            //    parameter.Append(" and OrgId = @OrgId ");
            //    dynamicParams.Add("OrgId", request.OrgId);
            //}

            if (request.StartTime != null)
            {
                parameter.Append(" and CreateTime >= @StartTime ");
                dynamicParams.Add("StartTime", request.StartTime);
            }

            if (request.EndTime != null)
            {
                parameter.Append(" and CreateTime <= @EndTime ");
                dynamicParams.Add("EndTime", request.EndTime);
            }


            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_user_info`  where 1=1  {0} ;
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (request.PageNumber - 1) * request.PageSize);
            dynamicParams.Add("pageNum", request.PageNumber);
            dynamicParams.Add("pageSize", request.PageSize);


            sql.AppendFormat(@" SELECT `tb_user_info`.`OrgId`,
    `tb_user_info`.`BranchId`,
    `tb_user_info`.`UserId`,
    `tb_user_info`.`Password`,
    `tb_user_info`.`Name`,
    `tb_user_info`.`Sex`,
    `tb_user_info`.`Ethnic`,
    `tb_user_info`.`Birthday`,
    `tb_user_info`.`Origin`,
    `tb_user_info`.`OriginAddress`,
    `tb_user_info`.`Phone`,
    `tb_user_info`.`IdNumber`,
    `tb_user_info`.`Education`,
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
    `tb_user_info`.`IsLeader`,
    `tb_user_info`.`LoginErrorCount`,
    `tb_user_info`.`LockingTime`
FROM `eagles`.`tb_user_info`  where 1=1  {0}    order by CreateTime desc limit  @pageStart ,@pageSize;
 ", parameter);



            return dbManager.Query<TbUserInfo>(sql.ToString(), dynamicParams);
            // throw new NotImplementedException();
        }

        public List<TbUserInfo> GetUserInfoList(GetAuthorityUserSetUpRequset requset, out int totalCount)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.UserId > 0)
            {
                parameter.Append(" and UserId = @UserId ");
                dynamicParams.Add("UserId", requset.UserId);
            }

            if (requset.BranchId > 0)
            {
                parameter.Append(" and BranchId = @BranchId ");
                dynamicParams.Add("BranchId", requset.BranchId);
            }

            if (requset.OrgId > 0)
            {
                parameter.Append(" and OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }


            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_user_info`  where 1=1  {0} ;
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);

            sql.AppendFormat(@" 
SELECT `tb_user_info`.`OrgId`,
    `tb_user_info`.`BranchId`,
    `tb_user_info`.`UserId`,
    `tb_user_info`.`Password`,
    `tb_user_info`.`Name`,
    `tb_user_info`.`Sex`,
    `tb_user_info`.`Ethnic`,
    `tb_user_info`.`Birthday`,
    `tb_user_info`.`Origin`,
    `tb_user_info`.`OriginAddress`,
    `tb_user_info`.`Phone`,
    `tb_user_info`.`IdNumber`,
    `tb_user_info`.`Education`,
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
    `tb_user_info`.`IsLeader`,
    `tb_user_info`.`LoginErrorCount`,
    `tb_user_info`.`LockingTime`
FROM `eagles`.`tb_user_info`  where 1=1  {0}    order by CreateTime desc limit  @pageStart ,@pageSize
 ", parameter);


       

            return dbManager.Query<TbUserInfo>(sql.ToString(), dynamicParams);
        }

        public int RemoveAuthorityUserSetUp(List<TbUserRelationship> list)
        {

            return dbManager.Excuted(@"  DELETE FROM `eagles`.`tb_user_relationship`
            WHERE  `UserId` in @UserId;", new { UserId = list.Select(x => x.UserId).ToArray() });
        }

        public int CreateAuthorityUserSetUp(List<TbUserRelationship> list)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_user_relationship`
(`OrgId`,
`UserId`,
`SubUserId`)
VALUES
(@OrgId,
@UserId,
@SubUserId);", list);
        }

        public List<TbUserRelationship> GetAuthorityUserSetUp(int requsetUserId)
        {

            var sql = new StringBuilder();

            sql.Append(@" SELECT `tb_user_relationship`.`OrgId`,
    `tb_user_relationship`.`UserId`,
    `tb_user_relationship`.`SubUserId`
FROM `eagles`.`tb_user_relationship`  where `UserId` = @UserId;
 ");
            return dbManager.Query<TbUserRelationship>(sql.ToString(), new { UserId = requsetUserId });
        }

        public int CreateUserInfo(List<TbUserInfo> userinfo)
        {

            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_user_info`
(`OrgId`,
`BranchId`,
`UserId`,
`Password`,
`Name`,
`Sex`,
`Ethnic`,
`Birthday`,
`Origin`,
`OriginAddress`,
`Phone`,
`IdNumber`,
`Education`,
`School`,
`Provice`,
`City`,
`District`,
`Address`,
`Company`,
`Dept`,
`Title`,
`PreMemberTime`,
`MemberTime`,
`MemberType`,
`Status`,
`MemberStatus`,
`PhotoUrl`,
`NickPhotoUrl`,
`CreateTime`,
`EditTime`,
`OperId`,
`IsCustomer`,
`Score`,
`IsLeader`)
VALUES
(@OrgId,
@BranchId,
@UserId,
@Password,
@Name,
@Sex,
@Ethnic,
@Birthday,
@Origin,
@OriginAddress,
@Phone,
@IdNumber,
@Education,
@School,
@Provice,
@City,
@District,
@Address,
@Company,
@Dept,
@Title,
@PreMemberTime,
@MemberTime,
@MemberType,
@Status,
@MemberStatus,
@PhotoUrl,
@NickPhotoUrl,
now(),
now(),
@OperId,
@IsCustomer,
@Score,
@IsLeader);



", userinfo);

        }
    }
}
