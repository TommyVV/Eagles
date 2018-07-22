using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Base.Cache;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Activity;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class ActivityTaskDataAccess: IActivityTaskDataAccess
    {
        private readonly IDbManager dbManager;

        private readonly ICacheHelper cacheHelper;

        public ActivityTaskDataAccess(IDbManager dbManager,ICacheHelper cacheHelper)
        {
            this.dbManager = dbManager;
            this.cacheHelper = cacheHelper;
        }
        public int EditActivity(TbActivity mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_activity`
SET
`OrgId` = @OrgId,
`BranchId` = @BranchId,
`ActivityName` = @ActivityName,
`HtmlContent` = @HtmlContent,
`BeginTime` = @BeginTime,
`EndTime` = @EndTime,
`FromUser` = @FromUser,
`FromUserName` = @FromUserName,
`ActivityType` = @ActivityType,
`MaxCount` = @MaxCount,
`CanComment` = @CanComment,
`TestId` = @TestId,
`MaxUser` = @MaxUser,
`Attach1` = @Attach1,
`Attach2` = @Attach2,
`Attach3` = @Attach3,
`Attach4` = @Attach4,
`ImageUrl` = @ImageUrl,
`IsPublic` = @IsPublic,
`OrgReview` = @OrgReview,
`BranchReview` = @BranchReview,
`ToUserId` = @ToUserId,
`ToUserName` = @ToUserName,
`Status` = @Status,
`CreateType` = @CreateType,
`AttachName1` = @AttachName1,
`AttachName2` = @AttachName2,
`AttachName3` = @AttachName3,
`AttachName4` = @AttachName4,
`PublicTime` = @PublicTime
WHERE 
`ActivityId` = @ActivityId



", mod);
        }

      

        public int CreateActivity(TbActivity mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_activity`
(`OrgId`,
`BranchId`,
`ActivityId`,
`ActivityName`,
`HtmlContent`,
`BeginTime`,
`EndTime`,
`FromUser`,
`FromUserName`,
`ActivityType`,
`MaxCount`,
`CanComment`,
`TestId`,
`MaxUser`,
`Attach1`,
`Attach2`,
`Attach3`,
`Attach4`,
`ImageUrl`,
`IsPublic`,
`OrgReview`,
`BranchReview`,
`ToUserId`,
`ToUserName`,
`Status`,
`CreateType`,
`AttachName1`,
`AttachName2`,
`AttachName3`,
`AttachName4`,
`PublicTime`)
VALUES
(@OrgId,
@BranchId,
@ActivityId,
@ActivityName,
@HtmlContent,
@BeginTime,
@EndTime,
@FromUser,
@FromUserName,
@ActivityType,
@MaxCount,
@CanComment,
@TestId,
@MaxUser,
@Attach1,
@Attach2,
@Attach3,
@Attach4,
@ImageUrl,
@IsPublic,
@OrgReview,
@BranchReview,
@ToUserId,
@ToUserName,
@Status,
@CreateType,
@AttachName1,
@AttachName2,
@AttachName3,
@AttachName4,
@PublicTime);


", mod);
        
        }

        public int RemoveActivity(RemoveActivityTaskRequset requset)
        {
            return dbManager.Excuted(@"  DELETE FROM `eagles`.`tb_activity`
  where
                `ActivityId` = @ActivityId;
", new {ActivityId = requset.ActivityId});
}

        public TbActivity GetActivityDetail(GetActivityTaskDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_activity`.`OrgId`,
    `tb_activity`.`BranchId`,
    `tb_activity`.`ActivityId`,
    `tb_activity`.`ActivityName`,
    `tb_activity`.`HtmlContent`,
    `tb_activity`.`BeginTime`,
    `tb_activity`.`EndTime`,
    `tb_activity`.`FromUser`,
    `tb_activity`.`FromUserName`,
    `tb_activity`.`ActivityType`,
    `tb_activity`.`MaxCount`,
    `tb_activity`.`CanComment`,
    `tb_activity`.`TestId`,
    `tb_activity`.`MaxUser`,
    `tb_activity`.`Attach1`,
    `tb_activity`.`Attach2`,
    `tb_activity`.`Attach3`,
    `tb_activity`.`Attach4`,
    `tb_activity`.`ImageUrl`,
    `tb_activity`.`IsPublic`,
    `tb_activity`.`OrgReview`,
    `tb_activity`.`BranchReview`,
    `tb_activity`.`ToUserId`,
    `tb_activity`.`ToUserName`,
    `tb_activity`.`Status`,
    `tb_activity`.`CreateType`,
    `tb_activity`.`AttachName1`,
    `tb_activity`.`AttachName2`,
    `tb_activity`.`AttachName3`,
    `tb_activity`.`AttachName4`,
    `tb_activity`.`PublicTime`
FROM `eagles`.`tb_activity`   where ActivityId=@ActivityId;
 ");
            dynamicParams.Add("ActivityId", requset.ActivityId);

            return dbManager.QuerySingle<TbActivity>(sql.ToString(), dynamicParams);
        }

        public List<TbActivity> GetGetActivityList(GetActivityTaskRequset requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            var token = cacheHelper.GetData<TbUserToken>(requset.Token);
            if (token.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", token.OrgId);
            }

            //if (requset.BranchId > 0)
            //{
            //    parameter.Append(" and BranchId = @BranchId ");
            //    dynamicParams.Add("BranchId", requset.BranchId);
            //}


            if (requset.ActivityTaskType > 0)
            {
                parameter.Append(" and ActivityType = @ActivityType ");
                dynamicParams.Add("ActivityType", requset.ActivityTaskType);
            }

            


            if (!string.IsNullOrWhiteSpace(requset.ActivityName))
            {
                parameter.Append(" and ActivityName = @ActivityName ");
                dynamicParams.Add("ActivityName", requset.ActivityName);
            }

            //if (requset.UserName > 0)
            //{
            //    parameter.Append(" and ToUserId = @ToUserId ");
            //    dynamicParams.Add("ToUserId", requset.UserName);
            //}


            sql.AppendFormat(@"SELECT `tb_activity`.`OrgId`,
    `tb_activity`.`BranchId`,
    `tb_activity`.`ActivityId`,
    `tb_activity`.`ActivityName`,
    `tb_activity`.`HtmlContent`,
    `tb_activity`.`BeginTime`,
    `tb_activity`.`EndTime`,
    `tb_activity`.`FromUser`,
    `tb_activity`.`FromUserName`,
    `tb_activity`.`ActivityType`,
    `tb_activity`.`MaxCount`,
    `tb_activity`.`CanComment`,
    `tb_activity`.`TestId`,
    `tb_activity`.`MaxUser`,
    `tb_activity`.`Attach1`,
    `tb_activity`.`Attach2`,
    `tb_activity`.`Attach3`,
    `tb_activity`.`Attach4`,
    `tb_activity`.`ImageUrl`,
    `tb_activity`.`IsPublic`,
    `tb_activity`.`OrgReview`,
    `tb_activity`.`BranchReview`,
    `tb_activity`.`ToUserId`,
    `tb_activity`.`ToUserName`,
    `tb_activity`.`Status`,
    `tb_activity`.`CreateType`,
    `tb_activity`.`AttachName1`,
    `tb_activity`.`AttachName2`,
    `tb_activity`.`AttachName3`,
    `tb_activity`.`AttachName4`,
    `tb_activity`.`PublicTime`
FROM `eagles`.`tb_activity`  where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbActivity>(sql.ToString(), dynamicParams);
        }
    }
}
