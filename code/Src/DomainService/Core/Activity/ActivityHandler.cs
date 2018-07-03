using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Application.Model;
using Eagles.Base;
using Eagles.Base.DesEncrypt;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Activity;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.Interface.Core.Activity;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.ActivityAccess;
using Eagles.Application.Model.Activity.CreateActivity;
using Eagles.Application.Model.Activity.EditActivityComplete;
using Eagles.Application.Model.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Activity.EditActivityJoin;
using Eagles.Application.Model.Activity.EditActivityReview;
using Eagles.Application.Model.Activity.GetActivity;
using Eagles.Application.Model.Activity.GetActivityDetail;
using Eagles.Application.Model.Activity.GetPublicActivity;
using Eagles.Application.Model.Activity.GetPublicActivityDetail;

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
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException(MessageCode.UserNotExists, MessageKey.UserNotExists);
            }
            var fromUser = Convert.ToInt32(desEncrypt.Decrypt(request.ActivityFromUser)); //活动发起人
            if(!string.IsNullOrEmpty(request.ActivityToUserId))
                toUser = Convert.ToInt32(desEncrypt.Decrypt(request.ActivityToUserId)); //活动负责人
            if (fromUser == toUser)
                throw new TransactionException(MessageCode.InvalidActivityUser, MessageKey.InvalidActivityUser);
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
            act.CreateType = request.CreateType;
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
            if (result <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request)
        {
            var response = new EditActivityJoinResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var activityInfo = iActivityAccess.GetActivityDetail(request.ActivityId, request.AppId);
            if (activityInfo == null)
                throw new TransactionException("96", "活动不存在");
            if(activityInfo.Status!=0)
                throw new TransactionException("96", "活动状态不正确");
            var joinUserid = Convert.ToInt32(desEncrypt.Decrypt(request.JoinUserid)); //活动参与人
            var result = iActivityAccess.EditActivityJoin(tokens.OrgId, tokens.BranchId, request.ActivityId, joinUserid);
            if (result <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public EditActivityReviewResponse EditActivityReview(EditActivityReviewRequest request)
        {
            var response = new EditActivityReviewResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var activityInfo = iActivityAccess.GetActivityDetail(request.ActivityId, request.AppId);
            if (activityInfo == null)
                throw new TransactionException(MessageCode.ActivityNotExists, MessageKey.ActivityNotExists);
            var createType = activityInfo.CreateType;
            switch (request.Type)
            {
                case ActivityTypeEnum.Audit:
                    //上级审核任务
                    if (1 == createType && activityInfo.ToUserId != tokens.UserId)
                        throw new TransactionException("96", "必须发起人审核");
                    break;
                case ActivityTypeEnum.Apply:
                    //上级发起的活动
                    if (2 == createType && activityInfo.ToUserId != tokens.UserId)
                        throw new TransactionException("96", "必须负责人申请完成活动");
                    //下级发起的活动
                    else if(1 == createType && activityInfo.ToUserId != tokens.UserId)
                        throw new TransactionException("96", "必须负责人申请完成活动");
                    break;
            }
            var result = iActivityAccess.EditActivityReview(request.Type, request.ReviewType);
            if (result <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request)
        {
            var response = new EditActivityCompleteResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = iActivityAccess.EditActivityComplete(request.ActivityId);
            if (!result)
            {
                throw new TransactionException(MessageCode.SystemError, MessageKey.SystemError);
            }
            return response;
        }

        public EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            var response = new EditActivityFeedBackResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var activityInfo = iActivityAccess.GetActivityDetail(request.ActivityId, request.AppId);
            if (activityInfo == null)
            {
                response.Code = "96";
                response.Message = "活动不存在";
                return response;
            }
            if (activityInfo.Status != 0)
            {
                response.Code = "96";
                response.Message = "活动状态不正确";
                return response;
            }
            var result = iActivityAccess.EditActivityFeedBack(request.ActivityId, request.Content, request.AttachList);
            if (result <= 0)
            {
                 throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            var response = new GetActivityResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            //得到所有支部下活动
            var result = iActivityAccess.GetActivity(request.ActivityType, userInfo.BranchId);

            List<TbUserActivity> userActivity;

            switch (request.ActivityPage)
            {
                case ActivityPage.All:

                    break;

                case ActivityPage.Mine:
                    //得到用户参与活动
                    userActivity = iActivityAccess.GetUserActivity(userInfo.UserId);

                    result = (from act in result
                        join usact in userActivity on new {act.BranchId, act.ActivityId} equals new
                        {
                            usact.BranchId,
                            usact.ActivityId
                        }
                        select new TbActivity
                        {
                            ActivityId = act.ActivityId,
                            ActivityName = act.ActivityName,
                            ActivityType = act.ActivityType,
                            BeginTime = act.BeginTime,
                            HtmlContent = act.HtmlContent,
                            ImageUrl = act.ImageUrl
                        }).ToList();
                    break;

                case ActivityPage.Other:

                    //得到用户未参与活动
                    userActivity = iActivityAccess.GetUserActivity(userInfo.UserId);                
                    result = (from act in result
                              where !userActivity.Select(x => x.ActivityId).ToList().Contains(act.ActivityId)
                      
                        select new TbActivity
                        {
                            ActivityId = act.ActivityId,
                            ActivityName = act.ActivityName,
                            ActivityType = act.ActivityType,
                            BeginTime = act.BeginTime,
                            HtmlContent = act.HtmlContent,
                            ImageUrl = act.ImageUrl
                        }).ToList();

                    break;
                    
            }

            if (result.Count == 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }

            response.ActivityList = result?.Select(x => new Application.Model.Common.Activity
            {
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName,
                ActivityType = x.ActivityType,
                ActivityDate = x.BeginTime,
                Content = x.HtmlContent,
                ImgUrl = x.ImageUrl
            }).ToList();

         

            return response;
        }

        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            var response = new GetActivityDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = iActivityAccess.GetActivityDetail(request.ActivityId, request.AppId);
            if (result != null)
            {
                response.ActivityId = request.ActivityId;
                response.ActivityName = result.ActivityName;
                response.ActivityContent = result.HtmlContent;
                response.ActivityImageUrl = result.ImageUrl;
                response.ActivityStatus = result.Status;
                response.InitiateEncryptUserId = desEncrypt.Encrypt(result.FromUser.ToString());
                response.AcceptEncryptUserId = desEncrypt.Encrypt(result.ToUserId.ToString());
                response.CreateType = result.CreateType;
                response.ActivityJoinPeopleList = iActivityAccess.GetActivityJoinPeople(request.ActivityId);
                response.AttachmentList = new List<Attachment>();
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType1, AttachmentDownloadUrl = result.Attach1 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType2, AttachmentDownloadUrl = result.Attach2 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType3, AttachmentDownloadUrl = result.Attach3 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType4, AttachmentDownloadUrl = result.Attach4 });
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData); ;
            }
            return response;
        }

        public GetPublicActivityResponse GetPublicActivity(GetPublicActivityRequest request)
        {
            var response = new GetPublicActivityResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            }
            var result = iActivityAccess.GetPublicActivity(request.ActivityType, request.AppId);
            response.ActivityList = result?.Select(x => new Application.Model.Common.Activity
            {
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName,
                ActivityType = x.ActivityType,
                ActivityDate = x.BeginTime,
                Content = x.HtmlContent,
                ImgUrl = x.ImageUrl
            }).ToList();
            if (result == null || result.Count < 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetPublicActivityDetailResponse GetPublicActivityDetail(GetPublicActivityDetailRequest request)
        {
            var response = new GetPublicActivityDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iActivityAccess.GetPublicActivityDetail(request.ActivityId, request.AppId);
            if (result != null)
            {
                response.ActivityId = request.ActivityId;
                response.ActivityName = result.ActivityName;
                response.ActivityContent = result.HtmlContent;
                response.ActivityImageUrl = result.ImageUrl;
                response.ActivityStatus = result.Status;
                response.CreateType = result.CreateType;
                response.ActivityJoinPeopleList = iActivityAccess.GetActivityJoinPeople(request.ActivityId);
                response.AttachmentList = new List<Attachment>();
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType1, AttachmentDownloadUrl = result.Attach1 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType2, AttachmentDownloadUrl = result.Attach2 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType3, AttachmentDownloadUrl = result.Attach3 });
                response.AttachmentList.Add(new Attachment() { AttachmentType = result.AttachType4, AttachmentDownloadUrl = result.Attach4 });
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }
    }
}