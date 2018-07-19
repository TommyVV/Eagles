using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Dapper;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Activity;
using Eagles.Interface.DataAccess.ActivityAccess;

namespace Ealges.DomianService.DataAccess.ActivityData
{
    public class ActivityDataAccess : IActivityAccess
    {
        private readonly IDbManager dbManager;

        public ActivityDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int CreateActivity(TbActivity reqActivity, out int activityId)
        {
            activityId = dbManager.ExecuteScalar<int>(@"insert into eagles.tb_activity (OrgId, BranchId, ActivityName, HtmlContent, BeginTime, EndTime, FromUser, ActivityType, MaxCount, CanComment, 
TestId, MaxUser, Attach1, Attach2, Attach3, Attach4, AttachName1, AttachName2, AttachName3, AttachName4, ImageUrl, IsPublic, OrgReview, BranchReview, ToUserId, Status, CreateType) 
value (@OrgId, @BranchId, @ActivityName, @HtmlContent, @BeginTime, @EndTime, @FromUser, @ActivityType, @MaxCount, @CanComment, @TestId, @MaxUser, @Attach1, @Attach2, @Attach3, @Attach4, 
@AttachName1, @AttachName2, @AttachName3, @AttachName4, @ImageUrl, @IsPublic, @OrgReview, @BranchReview, @ToUserId, @Status, @CreateType); select LAST_INSERT_ID(); ", reqActivity);
            //return dbManager.Excuted(@"insert into eagles.tb_user_activity(OrgId,BranchId,ActivityId,UserId,CreateTime) values (@OrgId,@BranchId,@ActivityId,@UserId,@CreateTime)", userActivity);
            return 1;
        }

        public int EditActivityJoin(TbUserActivity userActivity)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_activity(OrgId,BranchId,ActivityId,UserId,CreateTime) values (@OrgId,@BranchId,@ActivityId,@UserId,@CreateTime)", userActivity);
        }

        public int EditActivityReview(ActivityTypeEnum type, int activityId, int reviewType)
        {
            var result = 0;
            switch (type)
            {
                case ActivityTypeEnum.Audit:
                    //上级审核任务
                    if (0 == reviewType)
                        result = dbManager.Excuted("update eagles.tb_activity set Status = 0 where ActivityId = @ActivityId and Status = -1 ", new { ActivityId = activityId }); //审核通过
                    else
                        result = dbManager.Excuted("update eagles.tb_activity set Status = -9 where ActivityId = @ActivityId and Status = -1 ", new { ActivityId = activityId }); //审核不通过
                    break;                
                case ActivityTypeEnum.Apply:
                    //下级申请完成任务
                    result = dbManager.Excuted("update eagles.tb_activity set Status = 1 where ActivityId = @ActivityId and Status = 0 ", new { ActivityId = activityId }); //2-完成任务待审核
                    break;
            }
            return result;
        }

        public bool EditActivityComplete(int activityId, int completeStatus, int score)
        {
            var commandString = "";
            if (completeStatus == 0)
                commandString = @"update eagles.tb_activity set Status = 2 where ActivityId = @ActivityId and Status = 1"; //通过
            else
                commandString = @"update eagles.tb_activity set Status = 0 where ActivityId = @ActivityId and Status = 1"; //不通过
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = commandString,
                    Parameter = new { ActivityId = activityId }
                },
                new TransactionCommand()
                {
                    CommandString = "update eagles.tb_user_activity set RewardsScore = @RewardsScore, CompleteTime=@CompleteTime where ActivityId = @ActivityId",
                    Parameter = new { RewardsScore = score, CompleteTime = DateTime.Now, ActivityId = activityId }
                }
            };
            return dbManager.ExcutedByTransaction(commands);
        }
        
        public int EditActivityFeedBack(TbUserActivity userActivity)
        {
            return dbManager.Excuted(@"update eagles.tb_user_activity set UserFeedBack = @UserFeedBack, AttachName1 = @AttachName1, AttachName2 = @AttachName2, AttachName3 = @AttachName3, 
AttachName4 = @AttachName4, Attach1 = @Attach1, Attach2 = @Attach2, Attach3 = @Attach3, Attach4 = @Attach4 where ActivityId = @ActivityId and UserId = @UserId ", userActivity);
        }

        public List<TbUserActivity> GetActivityFeedBack(int activityId, int appId, int userId)
        {
            return dbManager.Query<TbUserActivity>(@"select a.activityId,a.UserId,b.Name,a.UserFeedBack,a.AttachName1,a.AttachName2,a.AttachName3,a.AttachName4,
a.Attach1,a.Attach2,a.Attach3,a.Attach4 from eagles.tb_user_activity a join eagles.tb_user_info b on a.UserId = b.UserId
where a.ActivityId = @ActivityId and a.OrgId = @OrgId and a.UserId = @UserId ", new { ActivityId = activityId, Orgid = appId, UserId = userId });
        }

        public List<TbActivity> GetActivity(ActivityType activityType, int branchId, int pageIndex = 1, int pageSize = 10)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            if (activityType > 0)
            {
                parameter.Append(" and ActivityType = @ActivityType ");
                dynamicParams.Add("ActivityType", activityType);
            }
            if (branchId > 0)
            {
                parameter.Append(" and BranchId = @BranchId ");
                dynamicParams.Add("BranchId", branchId);
            }
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            dynamicParams.Add("PageIndex", pageIndexParameter);
            dynamicParams.Add("PageSize", pageSize);
            sql.AppendFormat(@"select OrgId,BranchId,ActivityId,ActivityName,HtmlContent,BeginTime,EndTime,FromUser,ActivityType,MaxCount,CanComment,TestId,MaxUser,
Attach1,Attach2,Attach3,Attach4,AttachName1,AttachName2,AttachName3,AttachName4,ImageUrl,IsPublic,OrgReview,BranchReview,ToUserId,Status from eagles.tb_activity where Status <> -9 {0} 
limit @PageIndex, @PageSize ", parameter);
            return dbManager.Query<TbActivity>(sql.ToString(), dynamicParams);
        }
        
        public TbActivity GetActivityDetail(int activityId, int appId)
        {
            var result = dbManager.Query<TbActivity>(@"select OrgId,BranchId,ActivityId,ActivityName,Status,ImageUrl,HtmlContent,AttachName1,AttachName2,
AttachName3,AttachName4,Attach1,Attach2,Attach3,Attach4,FromUser,ToUserId,ImageUrl,CreateType,MaxCount,MaxUser from eagles.tb_activity 
where ActivityId = @ActivityId and OrgId = @OrgId ", new { ActivityId = activityId, Orgid = appId });
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public List<TbActivity> GetPublicActivity(ActivityType activityType, int appId, int pageIndex = 1, int pageSize = 10)
        {
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            return dbManager.Query<TbActivity>(@"select activityId,activityName,ImageUrl,HtmlContent,TestId from eagles.tb_activity 
where ActivityType = @ActivityType and OrgId = @OrgId and IsPublic = @IsPublic and OrgReview = @OrgReview and BranchReview = @BranchReview 
limit @PageIndex, @PageSize ",
                new
                {
                    ActivityType = (int) activityType,
                    Orgid = appId,
                    IsPublic = 0,
                    OrgReview = 0,
                    BranchReview = 0,
                    PageIndex = pageIndexParameter,
                    PageSize = pageSize
                });
        }

        public TbActivity GetPublicActivityDetail(int activityId, int appId)
        {
            var result = dbManager.Query<TbActivity>(@"select OrgId,BranchId,ActivityId,ActivityName,Status,ImageUrl,HtmlContent,AttachName1,AttachName2,AttachName3,AttachName4,Attach1,Attach2,
Attach3,Attach4,FromUser,ToUserId,ImageUrl,CreateType,MaxCount,MaxUser from eagles.tb_activity where ActivityId = @ActivityId and OrgId = @OrgId 
and IsPublic = @IsPublic and OrgReview = @OrgReview and BranchReview = @BranchReview", new {ActivityId = activityId, Orgid = appId, IsPublic = 0, OrgReview = 0, BranchReview = 0});
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public List<JoinPeople> GetActivityJoinPeople(int activityId)
        {
            var result = dbManager.Query<JoinPeople>(@"select a.UserId, b.Name from eagles.tb_user_activity a 
join eagles.tb_user_info b on a.UserId = b.UserId where ActivityId = @ActivityId ", new {ActivityId = activityId});
            if (result != null && result.Any())
            {
                return result;
            }
            return null;
        }

        public int GetUserActivityCount(int activityId)
        {
            return dbManager.ExecuteScalar<int>("select count(*) from eagles.tb_user_activity where ActivityId = @ActivityId ", new { ActivityId = activityId });
        }

        public List<TbUserActivity> GetUserActivity(int userId)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            parameter.Append(" and UserId = @UserId ");
            dynamicParams.Add("UserId", userId);
            sql.AppendFormat(@" SELECT `tb_user_activity`.`orgId`,
`tb_user_activity`.`BranchId`,
`tb_user_activity`.`ActivityId`,
`tb_user_activity`.`UserId`,
`tb_user_activity`.`UserFeedBack`,
`tb_user_activity`.`CreateTime`,
`tb_user_activity`.`Status`,
`tb_user_activity`.`CompleteTime`,
`tb_user_activity`.`RewardsScore`,
`tb_user_activity`.`Attach1`,
`tb_user_activity`.`Attach2`,
`tb_user_activity`.`Attach3`,
`tb_user_activity`.`Attach4`,
`tb_user_activity`.`AttachName1`,
`tb_user_activity`.`AttachName2`,
`tb_user_activity`.`AttachName3`,
`tb_user_activity`.`AttachName4`
FROM `eagles`.`tb_user_activity`
  where  1=1  {0}  ", parameter);
            return dbManager.Query<TbUserActivity>(sql.ToString(), dynamicParams);
        }

    }
}