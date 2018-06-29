using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.Activity;
using Eagles.Interface.DataAccess.Util;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.Interface.DataAccess.ActivityAccess;
using Eagles.Application.Model.Activity.CreateActivity;
using Eagles.Application.Model.Activity.EditActivityComplete;
using Eagles.Application.Model.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Activity.EditActivityJoin;
using Eagles.Application.Model.Activity.EditActivityReview;
using Eagles.Application.Model.Activity.GetActivity;
using Eagles.Application.Model.Activity.GetActivityDetail;
using Eagles.DomainService.Model.Activity;

namespace Eagles.DomainService.Core.Activity
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly IActivityAccess iActivityAccess;
        private readonly IDesEncrypt desEncrypt;
        private readonly IUtil util;

        public ActivityHandler(IActivityAccess iActivityAccess, IDesEncrypt desEncrypt, IUtil util)
        {
            this.iActivityAccess = iActivityAccess;
            this.desEncrypt = desEncrypt;
            this.util = util;
        }

        public CreateActivityResponse CreateActivity(CreateActivityRequest request)
        {
            var response = new CreateActivityResponse();
            var toUser = 0;
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("01", "用户不存在");
            }
            var fromUser = Convert.ToInt32(desEncrypt.Decrypt(request.ActivityFromUser)); //活动发起人
            if(!string.IsNullOrEmpty(request.ActivityToUserId))
                toUser = Convert.ToInt32(desEncrypt.Decrypt(request.ActivityToUserId)); //活动负责人
            if (fromUser == toUser)
            {
                throw new TransactionException("01", "负责人不能和发起人一致");
            }
            var act = new TbActivity();
            act.OrgId = tokens.OrgId;
            act.BranchId = tokens.BranchId;
            act.ActivityName = request.ActivityName;
            act.ActivityType = request.ActivityType;
            act.BeginTime = request.ActivityBeginDate;
            act.EndTime = request.ActivityEndDate;
            act.HtmlContent = request.ActivityContent;
            act.FromUser = fromUser;
            act.ToUserId = toUser;
            act.CanComment = request.CanComment;
            act.IsPublic = request.IsPublic;
            act.TestId = request.TestId;
            act.MaxCount = request.MaxCount;
            act.MaxUser = request.MaxUser;
            act.ImageUrl = "";
            if (1 == userInfo.IsLeader)
                act.Status = 0; //1:初始状态;(上级发给下级的初始状态) 
            else
                act.Status = -1; //2:下级发起任务;上级审核任务是否允许开始
            var attachList = request.AttachList;
            for (int i = 0; i < attachList.Count; i++)
            {
                if (i == 0)
                {
                    act.AttachType1 = attachList[i].AttachmentType;
                    act.Attach1 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 1)
                {
                    act.AttachType2 = attachList[i].AttachmentType;
                    act.Attach2 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 2)
                {
                    act.AttachType3 = attachList[i].AttachmentType;
                    act.Attach4 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 3)
                {
                    act.AttachType4 = attachList[i].AttachmentType;
                    act.Attach4 = attachList[i].AttachmentDownloadUrl;
                }
            }
            var result = iActivityAccess.CreateActivity(act);
            if (result > 0)
            {
                response.Code = "00";
                response.Message = "成功";
            }
            else
            {
                response.Code = "96";
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
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var joinUserid = Convert.ToInt32(desEncrypt.Decrypt(request.JoinUserid)); //活动参与人
            var result = iActivityAccess.EditActivityJoin(tokens.OrgId, tokens.BranchId, request.ActivityId, joinUserid);
            if (result > 0)
            {
                response.Code = "00";
                response.Message = "成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "失败";
            }
            return response;
        }

        public EditActivityReviewResponse EditActivityReview(EditActivityReviewRequest request)
        {
            var response = new EditActivityReviewResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var activityInfo = iActivityAccess.GetActivityDetail(request.ActivityId);
            if (activityInfo == null)
            {
                response.Code = "96";
                response.Message = "活动不存在";
                return response;
            }
            switch (request.Type)
            {
                case ActivityTypeEnum.Audit:
                    //上级审核任务
                    if (activityInfo.FromUser != tokens.UserId)
                    {
                        response.Code = "96";
                        response.Message = "必须发起人审核";
                        return response;
                    }
                    break;
                case ActivityTypeEnum.Apply:
                    //下级申请完成任务
                    if(activityInfo.ToUserId != tokens.UserId)
                    {
                        response.Code = "96";
                        response.Message = "必须负责人申请完成活动";
                        return response;
                    }
                    break;
            }
            var result = iActivityAccess.EditActivityReview(request.Type, request.ActivityId);
            if (result > 0)
            {
                response.Code = "00";
                response.Message = "成功";
            }
            else
            {
                response.Code = "96";
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
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iActivityAccess.EditActivityComplete(request.ActivityId);
            if (result)
            {
                response.Code = "00";
                response.Message = "成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "失败";
            }
            return response;
        }

        public EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            var response = new EditActivityFeedBackResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var taskInfo = iActivityAccess.GetActivityDetail(request.ActivityId);
            if (taskInfo == null)
            {
                response.Code = "96";
                response.Message = "活动不存在";
                return response;
            }
            if (taskInfo.Status != 0)
            {
                response.Code = "96";
                response.Message = "活动状态不正确";
                return response;
            }
            var result = iActivityAccess.EditActivityFeedBack(request.ActivityId, request.Content, request.AttachList);
            if (result > 0)
            {
                response.Code = "00";
                response.Message = "成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "失败";
            }
            return response;
        }

        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            var response = new GetActivityResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("01", "用户不存在");
            }
            var result = iActivityAccess.GetActivity(request.ActivityType, request.ActivityPage, tokens.UserId.ToString());
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
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }
        
        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            var response = new GetActivityDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = iActivityAccess.GetActivityDetail(request.ActivityId);
            if (result != null)
            {
                response.ActivityId = request.ActivityId;
                response.ActivityName = result.ActivityName;
                response.ActivityContent = result.HtmlContent;
                response.ActivityImageUrl = result.ImageUrl;
                response.ActivityStatus = result.Status;
                response.AttachmentList = new List<Attachment>();
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType1, AttachmentDownloadUrl = result.Attach1 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType2, AttachmentDownloadUrl = result.Attach2 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType3, AttachmentDownloadUrl = result.Attach3 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType4, AttachmentDownloadUrl = result.Attach4 });
                response.Code = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.Code = "96";
                response.Message = "查无数据";
            }
            return response;
        }
    }
}