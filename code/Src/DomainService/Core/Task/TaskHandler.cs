using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Interface.Core.Task;
using Eagles.Interface.Configuration;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.TaskAccess;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Task;
using Eagles.Application.Model;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Task.CreateTask;
using Eagles.Application.Model.Task.RemoveTaskStep;
using Eagles.Application.Model.Task.EditTaskAccept;
using Eagles.Application.Model.Task.EditTaskComplete;
using Eagles.Application.Model.Task.EditTaskFeedBack;
using Eagles.Application.Model.Task.EditTaskStep;
using Eagles.Application.Model.Task.GetTask;
using Eagles.Application.Model.Task.GetTaskDetail;
using Eagles.Application.Model.Task.GetTaskStep;
using Eagles.Application.Model.Task.GetPublicTask;
using Eagles.Application.Model.Task.GetPublicTaskDetail;

namespace Eagles.DomainService.Core.Task
{
    public class TaskHandler : ITaskHandler
    {
        private readonly ITaskAccess iTaskAccess;
        private readonly IUtil util;
        private readonly IEaglesConfig configuration;

        public TaskHandler(ITaskAccess iTaskAccess, IUtil util, IEaglesConfig configuration)
        {
            this.iTaskAccess = iTaskAccess;
            this.util = util;
            this.configuration = configuration;
        }

        public CreateTaskResponse CreateTask(CreateTaskRequest request)
        {
            var response = new CreateTaskResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);            
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var fromUser = request.TaskFromUser; //任务发起人
            var toUser = request.TaskToUserId; //任务负责人
            if (fromUser == toUser)
                throw new TransactionException(MessageCode.InvalidActivityUser, MessageKey.InvalidActivityUser);
            var fromUserName = util.GetUserInfo(fromUser).Name;
            var toUserName = util.GetUserInfo(toUser).Name;
            var task = new TbTask
            {
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                TaskName = request.TaskName,
                BeginTime = request.TaskBeginDate,
                EndTime = request.TaskEndDate,
                TaskContent = request.TaskContent,
                CreateTime = DateTime.Now,
                FromUser = fromUser,
                FromUserName = fromUserName,
                CanComment = request.CanComment,
                IsPublic = request.IsPublic,
                CreateType = request.CreateType,
                OrgReview = "-1",
                BranchReview = "-1"
            };
            if (0 == request.CreateType)
                task.Status = -2; //0:初始状态;(上级发给下级的初始状态)
            else
                task.Status = -1; //-1下级发起任务;上级审核任务是否允许开始
            var attachList = request.AttachList;
            for (int i = 0; i < attachList.Count; i++)
            {
                if (i == 0)
                {
                    task.AttachName1 = attachList[i].AttachName;
                    task.Attach1 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 1)
                {
                    task.AttachName2 = attachList[i].AttachName;
                    task.Attach2 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 2)
                {
                    task.AttachName3 = attachList[i].AttachName;
                    task.Attach3 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 3)
                {
                    task.AttachName4 = attachList[i].AttachName;
                    task.Attach4 = attachList[i].AttachmentDownloadUrl;
                }
            }
            var result = iTaskAccess.CreateTask(task, toUser, toUserName);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);

            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                Title = "任务发起",
                Content = configuration.EaglesConfiguration.TaskNoticeUrl,
                FromUser = request.TaskFromUser,
                UserId = request.TaskToUserId,
                IsRead = 1,
                CreateTime = DateTime.Now
            };
            if (0 == request.CreateType)
                userNotice.NewsType = 20; //20 任务发起（上级发给下级）
            else
                userNotice.NewsType = 21; //21 任务申请开始（下级发给上级）
            util.CreateUserNotice(userNotice);
            return response;
        }

        public RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request)
        {
            var response = new RemoveTaskStepResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            var stepInfo = iTaskAccess.GetStepExist(request.StepId);
            if (stepInfo == null)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            var result = iTaskAccess.RemoveTaskStep(request.TaskId, request.StepId);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            return response;
        }

        public EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request)
        {
            var response = new EditTaskAcceptResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)            
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            var createType = taskInfo.CreateType;
            var taskStatus = taskInfo.Status;

            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                Content = configuration.EaglesConfiguration.TaskNoticeUrl,
                IsRead = 1,
                CreateTime = DateTime.Now
            };

            switch (request.Type)
            {
                //上级审核任务
                case TaskAcceptType.Audit:
                    if (taskStatus != -1)
                        throw new TransactionException(MessageCode.TaskStatusError, MessageKey.TaskStatusError);
                    //下级发起的活动才会由上级审核
                    if (1 == createType && taskInfo.UserId != tokens.UserId)
                        throw new TransactionException("96", "必须上级审核");

                    userNotice.Title = "任务审核通过";
                    userNotice.FromUser = taskInfo.UserId;
                    userNotice.UserId = taskInfo.FromUser;
                    userNotice.NewsType = 22; //22 任务审核通过（上级审核任务允许开始）

                    break;
                //下级接受任务
                case TaskAcceptType.Accept:
                    if (taskStatus != -2)
                        throw new TransactionException(MessageCode.TaskStatusError, MessageKey.TaskStatusError);
                    if (0 == createType && taskInfo.UserId != tokens.UserId)
                        throw new TransactionException("96", "必须负责人接受任务");
                    break;
                //下级申请完成任务
                case TaskAcceptType.Apply:
                    if (taskStatus != 0)
                        throw new TransactionException(MessageCode.TaskStatusError, MessageKey.TaskStatusError);
                    //上级发起的任务
                    if (0 == createType && taskInfo.UserId != tokens.UserId)
                        throw new TransactionException("96", "必须负责人申请完成任务");
                    //下级发起的任务
                    else if (1 == createType && taskInfo.FromUser != tokens.UserId)
                        throw new TransactionException("96", "必须负责人申请完成任务");

                    userNotice.Title = "活动申请完成";
                    if (0 == createType)
                    {
                        userNotice.FromUser = taskInfo.UserId;
                        userNotice.UserId = taskInfo.FromUser;
                    }
                    else
                    {
                        userNotice.FromUser = taskInfo.FromUser;
                        userNotice.UserId = taskInfo.UserId;
                    }
                    userNotice.NewsType = 25; //25 任务申请完成

                    break;
            }
            var result = iTaskAccess.EditTaskAccept(request.Type, request.TaskId, request.ReviewType);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            if (request.ReviewType == 0)
                util.CreateUserNotice(userNotice);
            return response;
        }

        public EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request)
        {
            var response = new EditTaskCompleteResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            if (taskInfo.Status != 2)
                throw new TransactionException(MessageCode.TaskStatusError, MessageKey.TaskStatusError);
            var createType = taskInfo.CreateType;
            //上级发起的任务
            if (0 == createType && taskInfo.FromUser != tokens.UserId)
                throw new TransactionException("96", "必须负责人申请完成任务");
            //下级发起的任务
            else if (1 == createType && taskInfo.UserId != tokens.UserId)
                throw new TransactionException("96", "必须负责人申请完成任务");
            //查询任务奖励积分
            var score = util.RewardScore("0").Score;
            var result = iTaskAccess.EditTaskComplete(request.TaskId, request.IsPublic, request.CompleteStatus, score);
            if (!result)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                NewsType = 26, //26 任务审核确认完成
                Title = "任务完成",
                Content = configuration.EaglesConfiguration.TaskNoticeUrl,
                IsRead = 1,
                CreateTime = DateTime.Now
            };
            var userScore = 0;

            if (0 == createType)
            {
                userScore = taskInfo.FromUser; //奖励积分的人
                userNotice.FromUser = taskInfo.FromUser;
                userNotice.UserId = taskInfo.UserId;
            }
            else
            {
                userScore = taskInfo.UserId; //奖励积分的人
                userNotice.FromUser = taskInfo.UserId;
                userNotice.UserId = taskInfo.FromUser;
            }
            util.EditUserScore(userScore, score); //增加完成任务的人积分
            var scoreLs = new TbUserScoreTrace() { OrgId = tokens.OrgId, UserId = tokens.UserId, CreateTime = DateTime.Now, Score = score, RewardsType = "0", Comment = "完成任务获得积分" };
            util.CreateScoreLs(scoreLs); //增加积分的人增加积分流水
            if (request.CompleteStatus == 0)
                util.CreateUserNotice(userNotice);
            return response;
        }
        
        public EditTaskStepResponse EditTaskStep(EditTaskStepRequest request)
        {
            var response = new EditTaskStepResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            if (taskInfo.Status != 0)
                throw new TransactionException(MessageCode.TaskStatusError, MessageKey.TaskStatusError);
            var createType = taskInfo.CreateType;
            //上级发起的活动
            if (0 == createType && taskInfo.UserId != tokens.UserId)
                throw new TransactionException("96", "必须负责人编辑计划");
            //下级发起的活动
            else if (1 == createType && taskInfo.FromUser != tokens.UserId)
                throw new TransactionException("96", "必须负责人编辑计划");
            var stepInfo = iTaskAccess.GetStepExist(request.StepId);
            var action = ActionEnum.Create;
            if(stepInfo != null)
                action = ActionEnum.Modify;
            else
                action = ActionEnum.Create;
            var taskStep = new TbUserTaskStep(){
                StepId = request.StepId,
                StepName = request.StepName,
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                TaskId = request.TaskId,
                UserId = tokens.UserId,
                CreateTime = DateTime.Now
            };
            var result = iTaskAccess.EditTaskStep(action, taskStep);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);

            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                NewsType = 23, //23 任务负责人定制计划（新增，修改，删除）
                Title = "任务负责人定制计划",
                Content = configuration.EaglesConfiguration.TaskNoticeUrl,
                IsRead = 1,
                CreateTime = DateTime.Now
            };
            if (0 == createType)
            {
                userNotice.FromUser = taskInfo.UserId;
                userNotice.UserId = taskInfo.FromUser;
            }
            else
            {
                userNotice.FromUser = taskInfo.FromUser;
                userNotice.UserId = taskInfo.UserId;
            }
            util.CreateUserNotice(userNotice);

            return response;
        }

        public EditTaskFeedBackResponse EditTaskFeedBack(EditTaskFeedBackRequest request)
        {
            var response = new EditTaskFeedBackResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            if (taskInfo.Status != 0)
                throw new TransactionException(MessageCode.TaskStatusError, MessageKey.TaskStatusError);
            var createType = taskInfo.CreateType;
            //上级发起的活动
            if (0 == createType && taskInfo.UserId != tokens.UserId)
                throw new TransactionException("96", "必须负责人反馈任务");
            //下级发起的活动
            else if (1 == createType && taskInfo.FromUser != tokens.UserId)
                throw new TransactionException("96", "必须负责人反馈任务");
            var userTaskStep = new TbUserTaskStep()
            {
                TaskId = request.TaskId,
                StepId = request.StepId,
                Content = request.Content,
                UpdateTime = DateTime.Now
            };
            var attachList = request.AttachList;
            for (int i = 0; i < attachList.Count; i++)
            {
                if (i == 0)
                {
                    userTaskStep.AttachName1 = attachList[i].AttachName;
                    userTaskStep.Attach1 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 1)
                {
                    userTaskStep.AttachName2 = attachList[i].AttachName;
                    userTaskStep.Attach2 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 2)
                {
                    userTaskStep.AttachName3 = attachList[i].AttachName;
                    userTaskStep.Attach3 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 3)
                {
                    userTaskStep.AttachName4 = attachList[i].AttachName;
                    userTaskStep.Attach4 = attachList[i].AttachmentDownloadUrl;
                }
            }
            var result = iTaskAccess.EditTaskFeedBack(userTaskStep);
            if (result <= 0)
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);

            //发用户通知
            var userNotice = new TbUserNotice()
            {
                OrgId = tokens.OrgId,
                NewsType = 24, //24 任务负责人反馈计划内容（新增，修改）
                Title = "任务负责人反馈计划内容",
                Content = configuration.EaglesConfiguration.TaskNoticeUrl,
                IsRead = 1,
                CreateTime = DateTime.Now
            };
            if (0 == createType)
            {
                userNotice.FromUser = taskInfo.UserId;
                userNotice.UserId = taskInfo.FromUser;
            }
            else
            {
                userNotice.FromUser = taskInfo.FromUser;
                userNotice.UserId = taskInfo.UserId;
            }
            util.CreateUserNotice(userNotice);

            return response;
        }

        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            var response = new GetTaskStepResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = iTaskAccess.GetTaskStep(request.TaskId);
            response.StepList = result?.Select(x => new Step
            {
                StepId = x.StepId,
                StepName = x.StepName
            }).ToList();
            if (result == null || result.Count <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            var response = new GetTaskResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = iTaskAccess.GetTask(request.UserId, request.TaskType, request.PageIndex, request.PageSize);
            response.TaskList = result?.Select(x => new Application.Model.Common.Task
            {
                TaskId = x.TaskId,
                TaskeName = x.TaskName,
                TaskStatus = x.Status,
                TaskDate = x.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                TaskFromUser = x.FromUser,
                TaskFromUserName = x.FromUserName,
                TaskToUser = x.UserId,
                TaskToUserName = x.UserName
            }).ToList();
            if (result == null || result.Count <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request)
        {
            var response = new GetTaskDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (result != null)
            {
                response.TaskId = result.TaskId;
                response.TaskName = result.TaskName;
                response.TaskContent = result.TaskContent;
                response.TaskStatus = result.Status;
                response.TaskBeginDate = result.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.TaskEndDate = result.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.CreateTime = result.CreateTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.TaskFounder = result.FromUser;
                response.InitiateUserId = result.FromUser;
                response.InitiateUserName = result.FromUserName;
                response.AcceptUserId = result.UserId;
                response.AcceptUserName = result.UserName;
                response.CreateType = result.CreateType;
                response.AcctachmentList = new List<Attachment>
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
        
        public GetPublicTaskResponse GetPublicTask(GetPublicTaskRequest request)
        {
            var response = new GetPublicTaskResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iTaskAccess.GetPublicTask(request.AppId, request.PageIndex, request.PageSize);
            response.TaskList = result?.Select(x => new Application.Model.Common.Task
            {
                TaskId = x.TaskId,
                TaskeName = x.TaskName,
                TaskStatus = x.Status,
                TaskDate = x.BeginTime.ToString("yyyy-MM-dd HH:mm:ss"),
                TaskFromUser = x.FromUser,
                TaskToUser = x.UserId
            }).ToList();
            if (result == null || result.Count <= 0)
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }

        public GetPublicTaskDetailResponse GetPublicTaskDetail(GetPublicTaskDetailRequest request)
        {
            var response = new GetPublicTaskDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (!util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iTaskAccess.GetPublicTaskDetail(request.TaskId, request.AppId);
            if (result != null)
            {
                response.TaskName = result.TaskName;
                response.TaskContent = result.TaskContent;
                response.TaskStatus = result.Status;
                response.TaskBeginDate = result.BeginTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.TaskEndDate = result.EndTime.ToString("yyyy-MM-dd HH:mm:ss");
                response.TaskFounder = result.FromUser;
                response.InitiateUserId = result.FromUser;
                response.AcceptUserId = result.UserId;
                response.AcctachmentList = new List<Attachment>
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
    }
}