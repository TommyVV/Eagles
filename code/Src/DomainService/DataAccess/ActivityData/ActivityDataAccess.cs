using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Application.Model.Common;
using Eagles.Interface.Core.DataBase.ActivityAccess;

namespace Ealges.DomianService.DataAccess.ActivityData
{
    public class ActivityDataAccess :IActivityAccess
    {
        private readonly IDbManager dbManager;

        public ActivityDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int CreateActivity(Eagles.DomainService.Model.Activity.Activity reqActivity)
        {
            return dbManager.Excuted(@"insert into eagles.tb_activity (ActivityName, HtmlContent, BeginTime, EndTime, FromUser, ActivityType, MaxCount, CanComment, 
TestId, MaxUser, Attach1, Attach2, Attach3, Attach4, AttachType1, AttachType2, AttachType3, AttachType4, ImageUrl, IsPublic, OrgReview, BranchReview) 
value (@ActivityName, @HtmlContent, @BeginTime, @EndTime, @FromUser, @ActivityType, @MaxCount, @CanComment, 
@TestId, @MaxUser, @Attach1, @Attach2, @Attach3, @Attach4, @AttachType1, @AttachType2, @AttachType3, @AttachType4, @ImageUrl, @IsPublic, @OrgReview, @BranchReview)",
                new
                {
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
                    BranchReview = "-1"
                });
        }

        public int EditActivityComment(int orgId, int activityId, int userId, string content)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(OrgId,Id,Content,Createtime,UserId,ReviewStatus) value (@OrgId,@Id,@Content,@Createtime,@UserId,@ReviewStatus)",
                new
                {
                    OrgId = orgId,
                    Id = activityId,
                    Content = content,
                    Createtime = DateTime.Now,
                    UserId = userId,
                    ReviewStatus = "-1"
                });
        }

        public int EditActivityComplete(int activityId)
        {
            return dbManager.Excuted("update eagles.tb_activity set Status = '0' where ActivityId = @ActivityId ", new {ActivityId = activityId});
        }

        public int EditActivityFeedBack(int activityId, string content, List<Attachment> list)
        {
            return dbManager.Excuted(
                "update eagles.tb_user_activity set UserFeedBack = @UserFeedBack where ActivityId = @ActivityId ",
                new {UserFeedBack = content, ActivityId = activityId});
        }

        public int EditActivityJoin(int activityId)
        {
            return dbManager.Excuted("update eagles.tb_activity set Status = '0' where ActivityId = @ActivityId ", new {ActivityId = activityId});
        }

        public List<Eagles.DomainService.Model.Activity.Activity> GetActivity(int activityType)
        {
            return dbManager.Query<Eagles.DomainService.Model.Activity.Activity>(
                @"select activityId,activityName,ImageUrl,HtmlContent from eagles.TB_ACTIVITY where ActivityType = @ActivityType",
                new {ActivityType = activityType});
        }

        public Eagles.DomainService.Model.Activity.Activity GetActivityDetail(int activityId)
        {
            var result = dbManager.Query<Eagles.DomainService.Model.Activity.Activity>(
                @"select ActivityId,ActivityName,ImageUrl,HtmlContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,Attach2,Attach3,Attach4 from eagles.tb_activity where ActivityId = @ActivityId",
                new { ActivityId = activityId});
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public List<Eagles.DomainService.Model.User.UserComment> GetActivityComment(int activityId)
        {
            return dbManager.Query<Eagles.DomainService.Model.User.UserComment>(
                @"select Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment where Id = @Id",
                new {Id = activityId});
        }
    }
}