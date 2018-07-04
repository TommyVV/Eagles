using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
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
    public class ActivityDataAccess :IActivityAccess
    {
        private readonly IDbManager dbManager;

        public ActivityDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int CreateActivity(TbActivity reqActivity)
        {
            return dbManager.Excuted(
                @"insert into eagles.tb_activity (OrgId, BranchId, ActivityName, HtmlContent, BeginTime, EndTime, FromUser, ActivityType, MaxCount, CanComment, 
TestId, MaxUser, Attach1, Attach2, Attach3, Attach4, AttachType1, AttachType2, AttachType3, AttachType4, ImageUrl, IsPublic, OrgReview, BranchReview, ToUserId, Status,CreateType) 
value (@OrgId, @BranchId, @ActivityName, @HtmlContent, @BeginTime, @EndTime, @FromUser, @ActivityType, @MaxCount, @CanComment, @TestId, @MaxUser, @Attach1, @Attach2, @Attach3, @Attach4, 
@AttachType1, @AttachType2, @AttachType3, @AttachType4, @ImageUrl, @IsPublic, @OrgReview, @BranchReview, @ToUserId, @Status,@CreateType)",
                new
                {
                    OrgId = reqActivity.OrgId,
                    BranchId = reqActivity.BranchId,
                    ActivityName = reqActivity.ActivityName,
                    HtmlContent = reqActivity.HtmlContent,
                    BeginTime = reqActivity.BeginTime,
                    EndTime = reqActivity.EndTime,
                    FromUser = reqActivity.FromUser,
                    ActivityType = reqActivity.ActivityType,
                    MaxCount = reqActivity.MaxCount,
                    CanComment = reqActivity.CanComment,
                    TestId = reqActivity.TestId,
                    MaxUser = reqActivity.MaxUser,
                    Attach1 = reqActivity.Attach1,
                    Attach2 = reqActivity.Attach2,
                    Attach3 = reqActivity.Attach3,
                    Attach4 = reqActivity.Attach4,
                    AttachType1 = reqActivity.AttachType1,
                    AttachType2 = reqActivity.AttachType2,
                    AttachType3 = reqActivity.AttachType3,
                    AttachType4 = reqActivity.AttachType4,
                    ImageUrl = reqActivity.ImageUrl,
                    IsPublic = reqActivity.IsPublic,
                    OrgReview = "-1",
                    BranchReview = "-1",
                    ToUserId = reqActivity.ToUserId,
                    Status = reqActivity.Status,
                    CreateType = reqActivity.CreateType
                });
        }

        public int EditActivityJoin(int orgId, int branchId, int activityId, int userId)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_activity(OrgId,BranchId,ActivityId,UserId,CreateTime) values (@OrgId,@BranchId,@ActivityId,@UserId,@CreateTime)",
                new
                {
                    OrgId = orgId,
                    BranchId = branchId,
                    ActivityId = activityId,
                    UserId = userId,
                    CreateTime = DateTime.Now
                });
        }

        public int EditActivityReview(ActivityTypeEnum type, int activityId, int reviewType)
        {
            var result = 0;
            switch (type)
            {
                case ActivityTypeEnum.Audit:
                    //上级审核任务
                    result = dbManager.Excuted("update eagles.tb_activity set Status = 0 where ActivityId = @ActivityId and Status = -1 ", new { ActivityId = activityId }); //3-审核通过
                    break;                
                case ActivityTypeEnum.Apply:
                    //下级申请完成任务
                    result = dbManager.Excuted("update eagles.tb_activity set Status = 1 where ActivityId = @ActivityId and Status = 0 ", new { ActivityId = activityId }); //2-完成任务待审核
                    break;
            }
            return result;
        }

        public bool EditActivityComplete(int activityId)
        {
            //查询任务奖励积分
            var score = dbManager.ExecuteScalar<int>("select Score from eagles.tb_reward_score where RewardType = 0", new { });
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = @"update eagles.tb_activity set Status = 2 where ActivityId = @ActivityId and Status = 1 ",
                    Parameter =   new { ActivityId = activityId }
                },
                new TransactionCommand()
                {
                    CommandString = "update eagles.tb_user_activity set RewardsScore = @RewardsScore, CompleteTime=@CompleteTime where ActivityId = @ActivityId",
                    Parameter =  new { RewardsScore = score, CompleteTime = DateTime.Now, ActivityId = activityId }
                }
            };
            return dbManager.ExcutedByTransaction(commands);
        }
        
        public int EditActivityFeedBack(int activityId, string content, List<Attachment> attachList)
        {
            string attach1 = string.Empty, attach2 = string.Empty, attach3 = string.Empty, attach4 = string.Empty,
                attachType1 = string.Empty, attachType2 = string.Empty, attachType3 = string.Empty, attachType4 = string.Empty;
            for (int i = 0; i < attachList.Count; i++)
            {
                if (i == 0)
                {
                    attachType1 = attachList[i].AttachmentType;
                    attach1 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 1)
                {
                    attachType2 = attachList[i].AttachmentType;
                    attach2 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 2)
                {
                    attachType3 = attachList[i].AttachmentType;
                    attach3 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 3)
                {
                    attachType4 = attachList[i].AttachmentType;
                    attach4 = attachList[i].AttachmentDownloadUrl;
                }
            }
            return dbManager.Excuted(@"update eagles.tb_user_activity set UserFeedBack = @UserFeedBack, AttachType1 = @AttachType1, AttachType2 = @AttachType2, 
AttachType3 = @AttachType3, AttachType4 = @AttachType4, Attach1 = @Attach1, Attach2 = @Attach2, Attach3 = @Attach3, Attach4 = @Attach4 where ActivityId = @ActivityId ",
                new
                {
                    UserFeedBack = content,
                    AttachType1 = attachType1,
                    AttachType2 = attachType2,
                    AttachType3 = attachType3,
                    AttachType4 = attachType4,
                    Attach1 = attach1,
                    Attach2 = attach2,
                    Attach3 = attach3,
                    Attach4 = attach4,
                    ActivityId = activityId
                });
        }
        
        public List<TbActivity> GetActivity(ActivityType activityType, int branchId)
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
FROM `eagles`.`tb_activity`
  where  1=1 and Status=0 {0}  
 ", parameter);

            return dbManager.Query<TbActivity>(sql.ToString(), dynamicParams);
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
    `tb_user_activity`.`AttachType1`,
    `tb_user_activity`.`AttachType2`,
    `tb_user_activity`.`AttachType3`,
    `tb_user_activity`.`AttachType4`
FROM `eagles`.`tb_user_activity`

  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbUserActivity>(sql.ToString(), dynamicParams);

        }

        public TbActivity GetActivityDetail(int activityId, int appId)
        {
            var result = dbManager.Query<TbActivity>(@"select ActivityId,ActivityName,Status,ImageUrl,HtmlContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,
Attach2,Attach3,Attach4 from eagles.tb_activity where ActivityId = @ActivityId and OrgId = @OrgId ", new { ActivityId = activityId, Orgid = appId });
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public List<TbActivity> GetPublicActivity(ActivityType activityType, int appId)
        {
            return dbManager.Query<TbActivity>(
                @"select activityId,activityName,ImageUrl,HtmlContent from eagles.tb_activity 
where ActivityType = @ActivityType and OrgId = @OrgId and IsPublic = @IsPublic and OrgReview = @OrgReview and BranchReview = @BranchReview ",
                new {ActivityType = (int) activityType, Orgid = appId, IsPublic = 0, OrgReview = 0, BranchReview = 0});
        }

        public TbActivity GetPublicActivityDetail(int activityId, int appId)
        {
            var result = dbManager.Query<TbActivity>(
                @"select ActivityId,ActivityName,Status,ImageUrl,HtmlContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,Attach2,Attach3,Attach4 from eagles.tb_activity 
where ActivityId = @ActivityId and OrgId = @OrgId and IsPublic = @IsPublic and OrgReview = @OrgReview and BranchReview = @BranchReview",
                new {ActivityId = activityId, Orgid = appId, IsPublic = 0, OrgReview = 0, BranchReview = 0});
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
    }
}