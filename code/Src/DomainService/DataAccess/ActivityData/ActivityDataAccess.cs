using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
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
            return dbManager.Excuted(@"insert into eagles.tb_activity (OrgId, BranchId, ActivityName, HtmlContent, BeginTime, EndTime, FromUser, ActivityType, MaxCount, CanComment, 
TestId, MaxUser, Attach1, Attach2, Attach3, Attach4, AttachType1, AttachType2, AttachType3, AttachType4, ImageUrl, IsPublic, OrgReview, BranchReview, ToUserId, Status) 
value (@OrgId, @BranchId, @ActivityName, @HtmlContent, @BeginTime, @EndTime, @FromUser, @ActivityType, @MaxCount, @CanComment, @TestId, @MaxUser, @Attach1, @Attach2, @Attach3, @Attach4, 
@AttachType1, @AttachType2, @AttachType3, @AttachType4, @ImageUrl, @IsPublic, @OrgReview, @BranchReview, @ToUserId, @Status)",
                new
                {
                    OrgId = reqActivity.OrgId,
                    BranchId = reqActivity.BranchId,
                    ActivityName = reqActivity.ActivityName,
                    HtmlContent = reqActivity.HtmlContent,
                    BeginTime = reqActivity.BeginTime,
                    EndTime = reqActivity.EndTime ,
                    FromUser = reqActivity.FromUser,
                    ActivityType = reqActivity.ActivityType,
                    MaxCount = reqActivity.MaxCount,
                    CanComment =reqActivity.CanComment,
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
                    Status = reqActivity.Status
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
        
        public int EditActivityReview(ActivityTypeEnum type, int activityId)
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
        
        public List<TbActivity> GetActivity(ActivityType activityType, ActivityPage activityPage, string userId = null)
        {
            switch (activityPage)
            {
                case ActivityPage.All:
                    return dbManager.Query<TbActivity>(
                        @"select activityId,activityName,ImageUrl,HtmlContent from eagles.tb_activity where ActivityType = @ActivityType", new { ActivityType = (int)activityType });
                case ActivityPage.Mine:
                    return dbManager.Query<TbActivity>(
                        @"select a.activityId,a.activityName,a.ImageUrl,a.HtmlContent from eagles.tb_activity a 
join eagles.tb_user_activity b on a.ActivityId = b.ActivityId where a.ActivityType = @ActivityType and b.UserId = @UserId  ", new { ActivityType = (int)activityType, UserId = userId });
                case ActivityPage.Other:
                    return dbManager.Query<TbActivity>(
                        @"select a.activityId,a.activityName,a.ImageUrl,a.HtmlContent from eagles.tb_activity a 
join eagles.tb_user_activity b on a.ActivityId = b.ActivityId where a.ActivityType = @ActivityType and b.UserId <> @UserId  ", new { ActivityType = (int)activityType, UserId = userId });
                default: return null;
            }
        }

        public TbActivity GetActivityDetail(int activityId)
        {
            var result = dbManager.Query<TbActivity>(@"select ActivityId,ActivityName,Status,ImageUrl,HtmlContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,
Attach2,Attach3,Attach4 from eagles.tb_activity where ActivityId = @ActivityId", new { ActivityId = activityId});
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
    }
}