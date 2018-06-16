using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.Activity;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Curd.Activity.CreateActivity;
using Eagles.Application.Model.Curd.Activity.EditActivityJoin;
using Eagles.Application.Model.Curd.Activity.EditActivityComment;
using Eagles.Application.Model.Curd.Activity.EditActivityComplete;
using Eagles.Application.Model.Curd.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Curd.Activity.GetActivity;
using Eagles.Application.Model.Curd.Activity.GetActivityDetail;
using Eagles.Application.Model.Curd.Activity.GetActivityComment;
using Eagles.Interface.DataAccess.Util;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.Activity
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly IDbManager dbManager;
        private readonly IUtil util;

        public CreateActivityResponse CreateActivity(CreateActivityRequest request)
        {
            var response = new CreateActivityResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var token = request.Token;
            var attachType1 = "";
            var attachType2 = "";
            var attachType3 = "";
            var attachType4 = "";
            var attach1 ="";
            var attach2 ="";
            var attach3 ="";
            var attach4 = "";
            var activityType = request.ActivityType;
            var activityName = request.ActivityName;
            var beginTime = request.ActivityBeginDate;
            var endTime = request.ActivityEndDate;
            var maxCount = "";
            var testId = "";
            var maxUser = "";
            var content = request.ActivityContent;
            var fromUser = request.ActivityFromUser;
            var canComment = request.CanComment;
            var isPublic = request.IsPublic;
            var imageUrl = "";
            var list = request.AttachList;
            if (request.AttachList != null && request.AttachList.Count > 0)
            {
                attachType1 = list[0].AttachmentType is null ? null : list[0].AttachmentType;
                attachType2 = list[1].AttachmentType is null ? null : list[1].AttachmentType;
                attachType3 = list[2].AttachmentType is null ? null : list[2].AttachmentType;
                attachType4 = list[3].AttachmentType is null ? null : list[3].AttachmentType;
                attach1 = list[0].AttachmentDownloadUrl is null ? null : list[0].AttachmentDownloadUrl;
                attach2 = list[1].AttachmentDownloadUrl is null ? null : list[1].AttachmentDownloadUrl;
                attach3 = list[2].AttachmentDownloadUrl is null ? null : list[2].AttachmentDownloadUrl;
                attach4 = list[3].AttachmentDownloadUrl is null ? null : list[3].AttachmentDownloadUrl;
            }
            var result = dbManager.Excuted(@"insert into eagles.tb_activity (ActivityName, HtmlContent, BeginTime, EndTime, FromUser, ActivityType, MaxCount, CanComment, 
TestId, MaxUser, Attach1, Attach2, Attach3, Attach4, AttachType1, AttachType2, AttachType3, AttachType4, ImageUrl, IsPublic, OrgReview, BranchReview) 
value (@ActivityName, @HtmlContent, @BeginTime, @EndTime, @FromUser, @ActivityType, @MaxCount, @CanComment, 
@TestId, @MaxUser, @Attach1, @Attach2, @Attach3, @Attach4, @AttachType1, @AttachType2, @AttachType3, @AttachType4, @ImageUrl, @IsPublic, @OrgReview, @BranchReview)",
                new object[]
                {
                    activityName, content, beginTime, endTime, fromUser, activityType, maxCount, canComment, testId, maxUser, 
                    attach1, attach2, attach3, attach4, attachType1, attachType2, attachType3, attachType4, imageUrl, isPublic, "-1", "-1"
                });
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "失败";
            }
            return response;
        }

        public EditActivityCommentResponse EditActivityComment(EditActivityCommentRequest request)
        {
            var response = new EditActivityCommentResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var activityId = request.ActivityId;
            var userId = request.CommentUserId;
            var content = request.Comment;
            var result = dbManager.Excuted(@"insert into eagles.tb_user_comment(orgid,messageid,content,createtime,userid,reviewstatus,reviewuser,reviewtime ) 
value ('123','321',@content,'2018-6-14',123,'0',321,'2018-6-14')", new object[] { });
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "失败";
            }
            return response;
        }

        public EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request)
        {
            var response = new EditActivityCompleteResponse();
            //校验Token
            var token = request.Token;
            var activityId = request.ActivityId;
            var userId = request.ActivityType;
            var result = dbManager.Excuted("update eagles.tb_activity set Status = '' where ActivityId = @ActivityId ", new object[] {activityId});
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "失败";
            }
            return response;
        }

        public EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            var response = new EditActivityFeedBackResponse();
            var activityyId = request.ActivityId;
            var content = request.Content;
            var attachList = request.AttachList;
            var list = new List<Attachment>();
            //list = request.AttachList;
            throw new NotImplementedException();
        }

        public EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request)
        {
            var response = new EditActivityJoinResponse();
            //校验Token
            var token = request.Token;
            var activityId = request.ActivityId;

            var result = dbManager.Excuted("update eagles.tb_activity set Status = '' where ActivityId = @ActivityId ", new object[] { activityId });
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            var response = new GetActivityResponse();
            var activityId = request.UserId;
            var activityType = request.ActivityType;
            var result = dbManager.Query<DomainModel.Activity.Activity>("select activityId,activityName,ImageUrl,HtmlContent from eagles.TB_ACTIVITY where activityId = @id and ActivityType = @type", new object[] {activityId, activityType});
            response.ActivityList = result?.Select(x => new Application.Model.Common.Activity
            {
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName,
                ActivityType = x.ActivityType,
                ActivityDate = x.BeginTime,
                Content = x.HtmlContent,
                ImgUrl = x.ImageUrl
            }).ToList();
            if (result != null && result.Count > 0)
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetActivityCommentResponse GetActivityComment(GetActivityCommentRequest request)
        {
            var response = new GetActivityCommentResponse();
            var activityId = request.ActivityId;
            var result = dbManager.Query<DomainModel.User.UserComment>("select Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment where Id = @Id", activityId);
            response.ActivityCommentList = result?.Select(x => new Comment
            {
                CommentId = x.MessageId,
                CommentTime = x.ReviewTime,
                CommentUserId = x.UserId,
                CommentContent = x.Content
            }).ToList();
            if (result != null && result.Count > 0)
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            var response = new GetActivityDetailResponse();
            var activityId = request.ActivityId;
            //request.EncryptUserid;
            var result = dbManager.Query<DomainModel.Activity.Activity>("select activityId,activityName,ImageUrl,HtmlContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,Attach2,Attach3,Attach4 from eagles.TB_ACTIVITY where activityId = @id", activityId);

            if (result != null && result.Count > 0)
            {
                response.ActivityName = result[0].ActivityName;
                response.ActivityContent = result[0].HtmlContent;
                response.ActivityImageUrl = result[0].ImageUrl;
                response.AcctachmentList.Add(new Attachment() {AttachmentType = result[0].AttachType1, AttachmentName = result[0].Attach1});
                response.AcctachmentList.Add(new Attachment() { AttachmentType = result[0].AttachType2, AttachmentName = result[0].Attach2 });
                response.AcctachmentList.Add(new Attachment() { AttachmentType = result[0].AttachType3, AttachmentName = result[0].Attach3 });
                response.AcctachmentList.Add(new Attachment() { AttachmentType = result[0].AttachType4, AttachmentName = result[0].Attach4 });
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}