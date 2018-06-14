using System;
using System.Linq;
using Eagles.Application.Model.Common;
using Eagles.Interface.Core.Activity;
using Eagles.Application.Model.Curd.Activity.CreateActivity;
using Eagles.Application.Model.Curd.Activity.EditActivityJoin;
using Eagles.Application.Model.Curd.Activity.EditActivityComment;
using Eagles.Application.Model.Curd.Activity.EditActivityComplete;
using Eagles.Application.Model.Curd.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Curd.Activity.GetActivity;
using Eagles.Application.Model.Curd.Activity.GetActivityDetail;
using Eagles.Application.Model.Curd.Activity.GetActivityComment;
using DomainModel= Eagles.DomainService.Model;
using Eagles.Base.DataBase;

namespace Eagles.DomainService.Core
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly IDbManager dbManager;

        public CreateActivityResponse CreateActivity(CreateActivityRequest request)
        {
            //校验Token
            var token = request.Token;
            var activity = new DomainModel.Activity.Activity();
            
            activity.ActivityType = request.ActivityType;
            activity.ActivityName = request.ActivityName;
            activity.BeginTime = request.ActivityBeginDate;
            activity.EndTime = request.ActivityEndDate;
            activity.HtmlContent = request.ActivityContent;
            activity.FromUser = request.ActivityFromUser;
            activity.CanComment = request.CanComment;
            activity.IsPublic = request.IsPublic;
            var list = request.AttachList;
            if (request.AttachList != null)
            {
                activity.AttachType1 = request.AttachList[0].AttachmentType is null ? null : request.AttachList[0].AttachmentType;
                activity.AttachType2 = request.AttachList[1].AttachmentType is null ? null : request.AttachList[1].AttachmentType;
                activity.AttachType3 = request.AttachList[2].AttachmentType is null ? null : request.AttachList[2].AttachmentType;
                activity.AttachType4 = request.AttachList[3].AttachmentType is null ? null : request.AttachList[3].AttachmentType;
                activity.Attach1 = request.AttachList[0].AttachmentDownloadUrl is null ? null : request.AttachList[0].AttachmentDownloadUrl;
                activity.Attach2 = request.AttachList[1].AttachmentDownloadUrl is null ? null : request.AttachList[1].AttachmentDownloadUrl;
                activity.Attach3 = request.AttachList[2].AttachmentDownloadUrl is null ? null : request.AttachList[2].AttachmentDownloadUrl;
                activity.Attach4 = request.AttachList[3].AttachmentDownloadUrl is null ? null : request.AttachList[3].AttachmentDownloadUrl;
            }
            return new CreateActivityResponse()
            {
                ErrorCode = "00",
                Message = "创建成功"
            };
        }

        public EditActivityCommentResponse EditActivityComment(EditActivityCommentRequest request)
        {
            //校验Token
            var token = request.Token;

            var userComment = new DomainModel.User.UserComment();
            var activityId = request.ActivityId;
            userComment.UserId = request.CommentUserId;
            userComment.Content = request.Comment;
            
            throw new NotImplementedException();
        }

        public EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request)
        {
            throw new NotImplementedException();
        }

        public EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            throw new NotImplementedException();
        }

        public EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request)
        {
            throw new NotImplementedException();
        }

        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            var response = new GetActivityResponse();
            int i = 0;
            var activityId = request.UserId;
            var activityType = request.ActivityType;
            var result = dbManager.Query<DomainModel.Activity.Activity>("select activityId,activityName,ImageUrl,HtmlContent,Attach1,Attach2,Attach3,Attach4 from TB_ACTIVITY where activityId = @id", activityId);
            response.ActivityList = result?.Select(x => new Activity
            {
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName
                //todo any more 
            }).ToList();
            if (result.Count > 0)
            {
                foreach (var item in result)
                {
                    response.ActivityList[i].ActivityId = item.ActivityId;
                    response.ActivityList[i].ActivityName = item.ActivityName;
                    response.ActivityList[i].ActivityType = item.ActivityType;
                    response.ActivityList[i].ActivityDate = item.BeginTime;
                    response.ActivityList[i].Content = item.HtmlContent;
                    response.ActivityList[i].ImgUrl = item.ImageUrl;
                    i++;
                }
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
            throw new NotImplementedException();
        }

        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            var activityId = request.ActivityId;
            //request.EncryptUserid;
            var result = dbManager.Query<DomainModel.Activity.Activity>("select activityId,activityName,ImageUrl,HtmlContent,Attach1,Attach2,Attach3,Attach4 from TB_ACTIVITY where activityId = @id", activityId);
            
            GetActivityDetailResponse response = new GetActivityDetailResponse();
            response.ActivityName = result[0].ActivityName;
            response.ActivityContent = result[0].HtmlContent;
            response.ActivityImageUrl = result[0].ImageUrl;

            return response;
        }
    }
}