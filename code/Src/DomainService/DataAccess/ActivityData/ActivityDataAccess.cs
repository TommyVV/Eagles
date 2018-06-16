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
                    ActivityName = new[] {reqActivity.ActivityName},
                    HtmlContent = new[] {reqActivity.HtmlContent},
                    BeginTime = new[] {reqActivity.BeginTime},
                    FromUser = new[] {reqActivity.FromUser},
                    ActivityType = new[] {reqActivity.ActivityType},
                    MaxCount = new[] {reqActivity.MaxCount},
                    CanComment = new[] {reqActivity.CanComment},
                    TestId = new[] {reqActivity.TestId},
                    MaxUser = new[] {reqActivity.MaxUser},
                    Attach1 = new[] {reqActivity.Attach1},
                    Attach2 = new[] {reqActivity.Attach2},
                    Attach3 = new[] {reqActivity.Attach3},
                    Attach4 = new[] {reqActivity.Attach4},
                    AttachType1 = new[] {reqActivity.AttachType1},
                    AttachType2 = new[] {reqActivity.AttachType2},
                    AttachType3 = new[] {reqActivity.AttachType3},
                    AttachType4 = new[] {reqActivity.AttachType4},
                    ImageUrl = new[] {reqActivity.ImageUrl},
                    IsPublic = new[] {reqActivity.IsPublic},
                    OrgReview = new[] {"-1"},
                    BranchReview = new[] {"-1"}
                });

        }

        public int EditActivityComment(int activityId, int userId, string content)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(Id,Content,Createtime,UserId,ReviewStatus) value (@Id,@Content,@Createtime,@UserId,@ReviewStatus)",
                new
                {
                    Id = new[] {activityId},
                    Content = new[] {content},
                    UserId = new[] {userId},
                    ReviewStatus = new[] {"-1"}
                });
        }

        public int EditActivityComplete(int activityId)
        {
            return dbManager.Excuted("update eagles.tb_activity set Status = '0' where ActivityId = @ActivityId ", new { ActivityId = new[] { activityId } });
        }

        public int EditActivityFeedBack(int activityId, string content, List<Attachment> list)
        {
            return dbManager.Excuted("update eagles.tb_user_activity set UserFeedBack = @UserFeedBack where ActivityId = @ActivityId ",
                new {UserFeedBack = new[] {content}, ActivityId = new[] {activityId}});
        }

        public int EditActivityJoin(int activityId)
        {
            return dbManager.Excuted("update eagles.tb_activity set Status = '0' where ActivityId = @ActivityId ", new { ActivityId = new[] { activityId } });
        }

        public List<Eagles.DomainService.Model.Activity.Activity> GetActivity(int activityType)
        {
            return dbManager.Query<Eagles.DomainService.Model.Activity.Activity>("select activityId,activityName,ImageUrl,HtmlContent from eagles.TB_ACTIVITY where activityId = @id and ActivityType = @type",
                new {ActivityType = new[] {activityType}});
        }

        public Eagles.DomainService.Model.Activity.Activity GetActivityDetail(int activityId)
        {
            var result = dbManager.Query<Eagles.DomainService.Model.Activity.Activity>(
                "select activityId,activityName,ImageUrl,HtmlContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,Attach2,Attach3,Attach4 from eagles.TB_ACTIVITY where activityId = @id",
                new { Id = new[] { activityId } });
            return result[0];
        }

        public List<Eagles.DomainService.Model.User.UserComment> GetActivityComment(int activityId)
        {
            return dbManager.Query<Eagles.DomainService.Model.User.UserComment>(
                "select Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment where Id = @Id",
                new {Id = new[] {activityId}});
        }
    }
}