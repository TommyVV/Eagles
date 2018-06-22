using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.Common;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Activity;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class ActivityTaskDataAccess: IActivityTaskDataAccess
    {
        private readonly IDbManager dbManager;

        public ActivityTaskDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
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
`ActivityType` = @ActivityType,
`MaxCount` = @MaxCount,
`CanComment` = @CanComment,
`TestId` = @TestId,
`MaxUser` = @MaxUser,
`Attach1` = @Attach1,
`Attach2` = @Attach2,
`Attach3` = @Attach3,
`Attach4` = @Attach4,
`AttachType1` = @AttachType1,
`AttachType2` = @AttachType2,
`AttachType3` = @AttachType3,
`AttachType4` = @AttachType4,
`ImageUrl` = @ImageUrl,
`IsPublic` = @IsPublic,
`OrgReview` = @OrgReview,
`BranchReview` = @BranchReview,
`ToUserId` = @ToUserId,
`Status` = @Status
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
`ActivityType`,
`MaxCount`,
`CanComment`,
`TestId`,
`MaxUser`,
`Attach1`,
`Attach2`,
`Attach3`,
`Attach4`,
`AttachType1`,
`AttachType2`,
`AttachType3`,
`AttachType4`,
`ImageUrl`,
`IsPublic`,
`OrgReview`,
`BranchReview`,
`ToUserId`,
`Status`)
VALUES
(@OrgId,
@BranchId,
@ActivityId,
@ActivityName,
@HtmlContent,
@BeginTime,
@EndTime,
@FromUser,
@ActivityType,
@MaxCount,
@CanComment,
@TestId,
@MaxUser,
@Attach1,
@Attach2,
@Attach3,
@Attach4,
@AttachType1,
@AttachType2,
@AttachType3,
@AttachType4,
@ImageUrl,
@IsPublic,
@OrgReview,
@BranchReview,
@ToUserId,
@Status);

", mod);
        
        }

        public int RemoveActivity(RemoveActivityTaskRequset requset)
        {
            return dbManager.Excuted(@"  DELETE FROM `eagles`.`tb_activity`
  where
                `ActivityId` = @ActivityId;
", new {ActivityId = requset.ActivityTaskId});
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
    `tb_activity`.`ActivityType`,
    `tb_activity`.`MaxCount`,
    `tb_activity`.`CanComment`,
    `tb_activity`.`TestId`,
    `tb_activity`.`MaxUser`,
    `tb_activity`.`Attach1`,
    `tb_activity`.`Attach2`,
    `tb_activity`.`Attach3`,
    `tb_activity`.`Attach4`,
    `tb_activity`.`AttachType1`,
    `tb_activity`.`AttachType2`,
    `tb_activity`.`AttachType3`,
    `tb_activity`.`AttachType4`,
    `tb_activity`.`ImageUrl`,
    `tb_activity`.`IsPublic`,
    `tb_activity`.`OrgReview`,
    `tb_activity`.`BranchReview`,
    `tb_activity`.`ToUserId`,
    `tb_activity`.`Status`
FROM `eagles`.`tb_activity`   where ActivityId=@ActivityId;
 ");
            dynamicParams.Add("ColumnId", requset.ActivityTaskId);

            return dbManager.QuerySingle<TbActivity>(sql.ToString(), dynamicParams);
        }

        public List<TbActivity> GetGetActivityList(GetActivityTaskRequset requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }

            if (requset.BranchId > 0)
            {
                parameter.Append(" and BranchId = @BranchId ");
                dynamicParams.Add("BranchId", requset.BranchId);
            }

            if (!string.IsNullOrWhiteSpace(requset.ActivityTaskName))
            {
                parameter.Append(" and ActivityName = @ActivityName ");
                dynamicParams.Add("ActivityName", requset.ActivityTaskName);
            }

            if (requset.UserName > 0)
            {
                parameter.Append(" and ToUserId = @ToUserId ");
                dynamicParams.Add("ToUserId", requset.UserName);
            }


            sql.AppendFormat(@" SELECT `tb_activity`.`OrgId`,
    `tb_activity`.`BranchId`,
    `tb_activity`.`ActivityId`,
    `tb_activity`.`ActivityName`,
    `tb_activity`.`HtmlContent`,
    `tb_activity`.`BeginTime`,
    `tb_activity`.`EndTime`,
    `tb_activity`.`FromUser`,
    `tb_activity`.`ActivityType`,
    `tb_activity`.`MaxCount`,
    `tb_activity`.`CanComment`,
    `tb_activity`.`TestId`,
    `tb_activity`.`MaxUser`,
    `tb_activity`.`Attach1`,
    `tb_activity`.`Attach2`,
    `tb_activity`.`Attach3`,
    `tb_activity`.`Attach4`,
    `tb_activity`.`AttachType1`,
    `tb_activity`.`AttachType2`,
    `tb_activity`.`AttachType3`,
    `tb_activity`.`AttachType4`,
    `tb_activity`.`ImageUrl`,
    `tb_activity`.`IsPublic`,
    `tb_activity`.`OrgReview`,
    `tb_activity`.`BranchReview`,
    `tb_activity`.`ToUserId`,
    `tb_activity`.`Status`
FROM `eagles`.`tb_activity`   where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbActivity>(sql.ToString(), dynamicParams);
        }
    }
}
