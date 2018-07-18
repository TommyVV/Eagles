using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Interface.Core.Activity;
using Eagles.Interface.Configuration;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.ActivityAccess;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Activity;
using Eagles.Application.Model;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Activity.CreateActivity;
using Eagles.Application.Model.Activity.EditActivityJoin;
using Eagles.Application.Model.Activity.EditActivityReview;
using Eagles.Application.Model.Activity.EditActivityComplete;
using Eagles.Application.Model.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Activity.GetActivity;
using Eagles.Application.Model.Activity.GetActivityDetail;
using Eagles.Application.Model.Activity.GetPublicActivity;
using Eagles.Application.Model.Activity.GetPublicActivityDetail;
using Eagles.Application.Model.Activity.GetActivityFeedBack;

namespace Eagles.DomainService.Core.Activity
{
    public class ActivityHandler : IActivityHandler
    {
        private readonly IActivityAccess iActivityAccess;
        private readonly IUtil util;
        private readonly IEaglesConfig configuration;

        public ActivityHandler(IActivityAccess iActivityAccess, IUtil util, IEaglesConfig configuration)
        {
            this.iActivityAccess = iActivityAccess;
            this.util = util;
            this.configuration = configuration;
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
                throw new TransactionException(MessageCode.UserNotExists, MessageKey.UserNotExists);
            var fromUser = request.ActivityFromUser; //活动发起人
            if(request.ActivityToUserId>0)
                toUser = request.ActivityToUserId; //活动负责人
            if (fromUser == toUser)
                throw new TransactionException(MessageCode.InvalidActivityUser, MessageKey.InvalidActivityUser);
            var act = new TbActivity
            {
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                ActivityName = request.ActivityName,
                ActivityType = request.ActivityType,
                BeginTime = request.ActivityBeginDate,
                EndTime = request.ActivityEndDate,
                HtmlContent = request.ActivityContent,
                FromUser = fromUser,
                ToUserId = toUser,
                CanComment = request.CanComment,
                IsPublic = request.IsPublic,
                CreateType = request.CreateType,
                ImageUrl = request.ImageUrl,
                MaxCount = 99,
                MaxUser = 99,
                OrgReview = "-1",
                BranchReview = "-1"
            };
            if (0 == request.CreateType)
                act.Status = 0; //0:初始状态;(上级发给下级的初始状态)
            else
                act.Status = -1; //-1下级发起任务;上级审核任务是否允许开始

            var attachList = request.AttachList;
            if (attachList.Count > 0)
                for (int i = 0; i < attachList.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            act.AttachName1 = attachList[i].AttachName;
                            act.Attach1 = attachList[i].AttachmentDownloadUrl;
                            break;
                        case 1:
                            act.AttachName2 = attachList[i].AttachName;
                            act.Attach2 = attachList[i].AttachmentDownloadUrl;
                            break;
                        case 2:
                            act.AttachName3 = attachList[i].AttachName;
                            act.Attach4 = attachList[i].AttachmentDownloadUrl;
                            break;
                        case 3:
                            act.AttachName4 = attachList[i].AttachName;
                            act.Attach4 = attachList[i].AttachmentDownloadUrl;
                            break;
                    }
                }
            var result = iActivityAccess.CreateActivity(act);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);

            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                Title = "活动发起",
                Content = "活动已发起",
                TargetUrl = configuration.EaglesConfiguration.ActivityNoticeUrl,
                FromUser = request.ActivityFromUser,
                UserId = request.ActivityToUserId,
                IsRead = 1,
                CreateTime = DateTime.Now
            };
            if (0 == request.CreateType)
                userNotice.NewsType = 10; //10 活动发起（上级发给下级）
            else
                userNotice.NewsType = 11; //11 活动申请开始（下级发给上级）
            util.CreateUserNotice(userNotice);
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
                throw new TransactionException(MessageCode.ActivityNotExists, MessageKey.ActivityNotExists);
            if (activityInfo.Status != 0)
                throw new TransactionException(MessageCode.ActivityStatusError, MessageKey.ActivityStatusError);
            var branchId = activityInfo.BranchId;
            var userBranchId = tokens.BranchId;
            if (userBranchId != branchId)
                throw new TransactionException(MessageCode.InvalidActivityUser, MessageKey.InvalidActivityUser);
            var userActivityInfo = iActivityAccess.GetUserActivity(tokens.UserId);
            if (userActivityInfo != null && userActivityInfo.Count > 0)
                throw new TransactionException(MessageCode.JoinActivityExist, MessageKey.JoinActivityExist);
            var maxUser = activityInfo.MaxUser; //活动最大参与人数            
            var activityUserCount = iActivityAccess.GetUserActivityCount(request.ActivityId); //活动实际参与人数
            if (activityUserCount >= maxUser)
                throw new TransactionException(MessageCode.JoinActivityMax, MessageKey.JoinActivityMax);
            var userActivity = new TbUserActivity()
            {
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                ActivityId = request.ActivityId,
                UserId = tokens.UserId,
                CreateTime = DateTime.Now
            };
            var result = iActivityAccess.EditActivityJoin(userActivity);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
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

            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                TargetUrl = configuration.EaglesConfiguration.ActivityNoticeUrl,
                IsRead = 1,
                CreateTime = DateTime.Now
            };

            switch (request.Type)
            {
                case ActivityTypeEnum.Audit:
                    //上级审核任务
                    if (-1 != activityInfo.Status)
                        throw new TransactionException(MessageCode.ActivityStatusError, MessageKey.ActivityStatusError);
                    if (1 == createType && activityInfo.ToUserId != tokens.UserId)
                        throw new TransactionException("96", "必须发起人审核");

                    userNotice.Title = "活动审核通过";
                    userNotice.Content = "活动已审核通过";
                    userNotice.FromUser = activityInfo.ToUserId;
                    userNotice.UserId = activityInfo.FromUser;
                    userNotice.NewsType = 12; //12 活动审核通过（上级审核通过）

                    break;
                case ActivityTypeEnum.Apply:
                    if (0 != activityInfo.Status)
                        throw new TransactionException(MessageCode.ActivityStatusError, MessageKey.ActivityStatusError);
                    //上级发起的活动
                    if (0 == createType && activityInfo.ToUserId != tokens.UserId)
                        throw new TransactionException("96", "必须负责人申请完成活动");
                    //下级发起的活动
                    else if (1 == createType && activityInfo.FromUser != tokens.UserId)
                        throw new TransactionException("96", "必须负责人申请完成活动");

                    userNotice.Title = "活动申请完成";
                    userNotice.Content = "负责人已发起活动申请完成";
                    if (0 == createType)
                    {
                        userNotice.FromUser = activityInfo.ToUserId;
                        userNotice.UserId = activityInfo.FromUser;
                    }
                    else
                    {
                        userNotice.FromUser = activityInfo.FromUser; 
                        userNotice.UserId = activityInfo.ToUserId;
                    }
                    userNotice.NewsType = 14; //14 活动申请完成

                    break;
            }
            var result = iActivityAccess.EditActivityReview(request.Type, request.ActivityId, request.ReviewType);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            if (request.ReviewType == 0)
                util.CreateUserNotice(userNotice);
            return response;
        }

        public EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request)
        {
            var response = new EditActivityCompleteResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var activityInfo = iActivityAccess.GetActivityDetail(request.ActivityId, request.AppId);
            if (activityInfo == null)
                throw new TransactionException(MessageCode.ActivityNotExists, MessageKey.ActivityNotExists);
            if (activityInfo.Status != 1)
                throw new TransactionException(MessageCode.ActivityStatusError, MessageKey.ActivityStatusError);
            var createType = activityInfo.CreateType;
            //上级发起的活动
            if (0 == createType && activityInfo.FromUser != tokens.UserId)
                throw new TransactionException("96", "必须上级完成活动");
            //下级发起的活动
            else if (1 == createType && activityInfo.ToUserId != tokens.UserId)
                throw new TransactionException("96", "必须上级完成活动");
            //查询活动奖励积分
            var score = util.RewardScore("1").Score;
            var result = iActivityAccess.EditActivityComplete(request.ActivityId, request.CompleteStatus, score);
            if (!result)
                throw new TransactionException(MessageCode.SystemError, MessageKey.SystemError);
            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                NewsType = 15, //15 活动已完成
                Title = "活动完成",
                Content = "活动已完成",
                TargetUrl = configuration.EaglesConfiguration.ActivityNoticeUrl,
                IsRead = 1,
                CreateTime = DateTime.Now
            };
            if (0 == createType)
            {
                userNotice.FromUser = activityInfo.FromUser;
                userNotice.UserId = activityInfo.ToUserId;
            }
            else
            {
                userNotice.FromUser = activityInfo.ToUserId;
                userNotice.UserId = activityInfo.FromUser;
            }
            //所有参与活动的人增加积分
            var people = iActivityAccess.GetActivityJoinPeople(request.ActivityId);
            util.BatchEditUserScore(people, score); //增加所有完成活动人的积分
            foreach (var user in people)
            {
                var scoreLs = new TbUserScoreTrace() { OrgId = tokens.OrgId, UserId = user.UserId, CreateTime = DateTime.Now, Score = score, RewardsType = "0", Comment = "完成活动获得积分" };
                util.CreateScoreLs(scoreLs); //增加所有完成活动人的积分流水
            }
            if (request.CompleteStatus == 0)
                util.CreateUserNotice(userNotice);
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
                throw new TransactionException(MessageCode.ActivityNotExists, MessageKey.ActivityNotExists);
            if (activityInfo.Status != 0)
                throw new TransactionException(MessageCode.ActivityStatusError, MessageKey.ActivityStatusError);
            var createType = activityInfo.CreateType;
            //上级发起的活动
            if (0 == createType && activityInfo.ToUserId != tokens.UserId)
                throw new TransactionException("96", "必须负责人申请完成活动");
            //下级发起的活动
            else if (1 == createType && activityInfo.FromUser != tokens.UserId)
                throw new TransactionException("96", "必须负责人申请完成活动");
            var feeBack = new TbUserActivity()
            {
                UserId = tokens.UserId,
                UserFeedBack = request.Content,
                ActivityId = request.ActivityId
            };
            var attachList = request.AttachList;
            if (attachList.Count > 0)
                for (int i = 0; i < attachList.Count; i++)
                {
                    switch (i)
                    {
                        case 0:
                            feeBack.AttachName1 = attachList[i].AttachName;
                            feeBack.Attach1 = attachList[i].AttachmentDownloadUrl;
                            break;
                        case 1:
                            feeBack.AttachName2 = attachList[i].AttachName;
                            feeBack.Attach2 = attachList[i].AttachmentDownloadUrl;
                            break;
                        case 2:
                            feeBack.AttachName3 = attachList[i].AttachName;
                            feeBack.Attach4 = attachList[i].AttachmentDownloadUrl;
                            break;
                        case 3:
                            feeBack.AttachName4 = attachList[i].AttachName;
                            feeBack.Attach4 = attachList[i].AttachmentDownloadUrl;
                            break;
                    }
                }
            var result = iActivityAccess.EditActivityFeedBack(feeBack);
            if (result <= 0)
                 throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                NewsType = 13, //13 活动负责人反馈
                Title = "活动负责人反馈",
                Content = "活动已经反馈",
                TargetUrl = configuration.EaglesConfiguration.ActivityNoticeUrl,
                IsRead = 1,
                CreateTime = DateTime.Now
            };
            if (0 == createType)
            {
                userNotice.FromUser = activityInfo.ToUserId;
                userNotice.UserId = activityInfo.FromUser;
            }
            else
            {
                userNotice.FromUser = activityInfo.FromUser;
                userNotice.UserId = activityInfo.ToUserId;
            }
            util.CreateUserNotice(userNotice);
            return response;
        }

        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            var response = new GetActivityResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            //得到所有支部下活动
            var result = iActivityAccess.GetActivity(request.ActivityType, userInfo.BranchId, request.PageIndex, request.PageSize);
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
                            Status = act.Status,
                            TestId = act.TestId,
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
                            Status = act.Status,
                            TestId = act.TestId,
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
                ActivityDate = x.BeginTime.ToString("yyyy-MM-dd"),
                Status = x.Status,
                TestId = x.TestId,
                ImageUrl = x.ImageUrl
            }).ToList();
            return response;
        }

        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            var response = new GetActivityDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
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
                response.InitiateUserId = result.FromUser;
                response.AcceptUserId = result.ToUserId;
                response.CreateType = result.CreateType;
                response.ActivityJoinPeopleList = iActivityAccess.GetActivityJoinPeople(request.ActivityId);
                response.AttachmentList = new List<Attachment>
                {
                    new Attachment() {AttachName = result.AttachName1, AttachmentDownloadUrl = result.Attach1},
                    new Attachment() {AttachName = result.AttachName2, AttachmentDownloadUrl = result.Attach2},
                    new Attachment() {AttachName = result.AttachName3, AttachmentDownloadUrl = result.Attach3},
                    new Attachment() {AttachName = result.AttachName4, AttachmentDownloadUrl = result.Attach4}
                };
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
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = iActivityAccess.GetPublicActivity(request.ActivityType, request.AppId, request.PageIndex, request.PageSize);
            response.ActivityList = result?.Select(x => new Application.Model.Common.Activity
            {
                ActivityId = x.ActivityId,
                ActivityName = x.ActivityName,
                ActivityType = x.ActivityType,
                ActivityDate = x.BeginTime.ToString("yyyy-MM-dd"),
                TestId = x.TestId,
                ImageUrl = x.ImageUrl
            }).ToList();
            if (result == null || result.Count < 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }

        public GetPublicActivityDetailResponse GetPublicActivityDetail(GetPublicActivityDetailRequest request)
        {
            var response = new GetPublicActivityDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iActivityAccess.GetPublicActivityDetail(request.ActivityId, request.AppId);
            if (result != null)
            {
                response.ActivityId = request.ActivityId;
                response.ActivityName = result.ActivityName;
                response.ActivityContent = result.HtmlContent;
                response.ActivityImageUrl = result.ImageUrl;
                response.ActivityStatus = result.Status;
                response.InitiateUserId = result.FromUser;
                response.AcceptUserId = result.ToUserId;
                response.CreateType = result.CreateType;
                response.ActivityJoinPeopleList = iActivityAccess.GetActivityJoinPeople(request.ActivityId);
                response.AttachmentList = new List<Attachment>
                {
                    new Attachment() {AttachName = result.AttachName1, AttachmentDownloadUrl = result.Attach1},
                    new Attachment() {AttachName = result.AttachName2, AttachmentDownloadUrl = result.Attach2},
                    new Attachment() {AttachName = result.AttachName3, AttachmentDownloadUrl = result.Attach3},
                    new Attachment() {AttachName = result.AttachName4, AttachmentDownloadUrl = result.Attach4}
                };
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetActivityFeedBackResponse GetActivityFeedBack(GetActivityFeedBackRequest request)
        {
            var response = new GetActivityFeedBackResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var activityInfo = iActivityAccess.GetActivityDetail(request.ActivityId, request.AppId);
            if (activityInfo == null)
                throw new TransactionException(MessageCode.ActivityNotExists, MessageKey.ActivityNotExists);
            var createType = activityInfo.CreateType;
            var userId = 0;
            //上级发起的活动
            if (0 == createType)
                userId = activityInfo.ToUserId;
            //下级发起的活动
            else if (1 == createType)
                userId = activityInfo.FromUser;
            var result = iActivityAccess.GetActivityFeedBack(request.ActivityId, request.AppId, userId);
            response.FeedBackList = result?.Select(x => new FeedBack
            {
                UserId = x.UserId,
                UserName = x.Name,
                UserFeedBack = x.UserFeedBack,
                AttachList = new List<Attachment>()
                {
                    new Attachment() { AttachName = x.AttachName1, AttachmentDownloadUrl = x.Attach1 },
                    new Attachment() { AttachName = x.AttachName2, AttachmentDownloadUrl = x.Attach2 },
                    new Attachment() { AttachName = x.AttachName3, AttachmentDownloadUrl = x.Attach3 },
                    new Attachment() { AttachName = x.AttachName4, AttachmentDownloadUrl = x.Attach4 }
                }
            }).ToList();
            if (result == null || result.Count < 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }
    }
}