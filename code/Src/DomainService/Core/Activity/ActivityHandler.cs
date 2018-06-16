using System;
using System.Linq;
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
using Eagles.Interface.Core.DataBase.ActivityAccess;
using Eagles.Interface.DataAccess.Util;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.Activity
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly IActivityAccess iActivityAccess;
        private readonly IUtil util;

        public ActivityHandler(IActivityAccess iActivityAccess, IUtil util)
        {
            this.iActivityAccess = iActivityAccess;
            this.util = util;
        }

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
            var act = new DomainModel.Activity.Activity();
            act.ActivityName = request.ActivityName;
            act.ActivityType = request.ActivityType;
            act.BeginTime = request.ActivityBeginDate;
            act.EndTime = request.ActivityEndDate;
            act.HtmlContent = request.ActivityContent;
            act.FromUser = request.ActivityFromUser;
            act.CanComment = request.CanComment;
            act.IsPublic = request.IsPublic;
            act.MaxCount = 0;
            act.TestId = 0;
            act.MaxUser = 99;
            act.ImageUrl = "";
            var list = request.AttachList;
            if (request.AttachList != null && request.AttachList.Count > 0)
            {
                act.AttachType1 = list[0].AttachmentType is null ? null : list[0].AttachmentType;
                act.AttachType2 = list[1].AttachmentType is null ? null : list[1].AttachmentType;
                act.AttachType3 = list[2].AttachmentType is null ? null : list[2].AttachmentType;
                act.AttachType4 = list[3].AttachmentType is null ? null : list[3].AttachmentType;
                act.Attach1 = list[0].AttachmentDownloadUrl is null ? null : list[0].AttachmentDownloadUrl;
                act.Attach2 = list[1].AttachmentDownloadUrl is null ? null : list[1].AttachmentDownloadUrl;
                act.Attach3 = list[2].AttachmentDownloadUrl is null ? null : list[2].AttachmentDownloadUrl;
                act.Attach4 = list[3].AttachmentDownloadUrl is null ? null : list[3].AttachmentDownloadUrl;
            }
            var result = iActivityAccess.CreateActivity(act);
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
            var result = iActivityAccess.EditActivityComment(request.ActivityId, request.CommentUserId, request.Comment);
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
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iActivityAccess.EditActivityComplete(request.ActivityId);
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
            var result = iActivityAccess.EditActivityFeedBack(activityyId, content, attachList);
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

        public EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request)
        {
            var response = new EditActivityJoinResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iActivityAccess.EditActivityJoin(request.ActivityId);
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

        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            var response = new GetActivityResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iActivityAccess.GetActivity(Convert.ToInt32(request.ActivityType));
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
        
        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            var response = new GetActivityDetailResponse();
            var result = iActivityAccess.GetActivityDetail(request.ActivityId);
            if (result != null)
            {
                response.ActivityName = result.ActivityName;
                response.ActivityContent = result.HtmlContent;
                response.ActivityImageUrl = result.ImageUrl;
                response.AcctachmentList.Add(new Attachment() {AttachmentType = result.AttachType1, AttachmentName = result.Attach1});
                response.AcctachmentList.Add(new Attachment() { AttachmentType = result.AttachType2, AttachmentName = result.Attach2 });
                response.AcctachmentList.Add(new Attachment() { AttachmentType = result.AttachType3, AttachmentName = result.Attach3 });
                response.AcctachmentList.Add(new Attachment() { AttachmentType = result.AttachType4, AttachmentName = result.Attach4 });
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
            var result = iActivityAccess.GetActivityComment(request.ActivityId);
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
    }
}