using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Interface.Core.Task;
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

        public TaskHandler(ITaskAccess iTaskAccess, IUtil util)
        {
            this.iTaskAccess = iTaskAccess;
            this.util = util;
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
            var task = new TbTask();
            task.OrgId = tokens.OrgId;
            task.BranchId = tokens.BranchId;
            task.TaskName = request.TaskName;
            task.BeginTime = request.TaskBeginDate;
            task.EndTime = request.TaskEndDate;
            task.TaskContent = request.TaskContent;
            task.CreateTime = DateTime.Now;
            task.FromUser = fromUser;
            task.CanComment = request.CanComment;
            task.IsPublic = request.IsPublic;
            if (1 == userInfo.IsLeader)
                task.Status = -2; // -2:上级发起(待接受)
            else
                task.Status = -1; // -1:下级申请上级待审核状态
            var attachList = request.AttachList;
            for (int i = 0; i < attachList.Count; i++)
            {
                if (i == 0)
                {
                    task.AttachType1 = attachList[i].AttachmentType;
                    task.Attach1 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 1)
                {
                    task.AttachType2 = attachList[i].AttachmentType;
                    task.Attach2 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 2)
                {
                    task.AttachType3 = attachList[i].AttachmentType;
                    task.Attach3 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 3)
                {
                    task.AttachType4 = attachList[i].AttachmentType;
                    task.Attach4 = attachList[i].AttachmentDownloadUrl;
                }
            }
            var result = iTaskAccess.CreateTask(task, toUser);
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

        public RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request)
        {
            var response = new RemoveTaskStepResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            var result = iTaskAccess.RemoveTaskStep(request.TaskId, request.StepId);
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

        public EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request)
        {
            var response = new EditTaskAcceptResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)            
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            switch (request.Type)
            {
                case TaskTypeEnum.Audit:
                    //上级审核任务
                    if (taskInfo.FromUser != tokens.UserId)
                        throw new TransactionException("96", "必须发起人审核");
                    break;
                case TaskTypeEnum.Accept:
                    //下级接受任务
                    if (taskInfo.UserId != tokens.UserId)
                        throw new TransactionException("96", "必须负责人接受任务");
                    break;
                case TaskTypeEnum.Apply:
                    //下级申请完成任务
                    if (taskInfo.UserId != tokens.UserId)
                        throw new TransactionException("96", "必须负责人申请完成任务");
                    break;
            }
            var result = iTaskAccess.EditTaskAccept(request.Type, request.TaskId, request.ReviewType);
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

        public EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request)
        {
            var response = new EditTaskCompleteResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var taskInfo = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (taskInfo == null)
                throw new TransactionException(MessageCode.TaskNotExists, MessageKey.TaskNotExists);
            if (taskInfo.Status != 0)
                throw new TransactionException(MessageCode.TaskStatusError, MessageKey.TaskStatusError);
            var score = util.RewardScore("0"); //任务奖励积分
            var result = iTaskAccess.EditTaskComplete(request.TaskId, request.IsPublic, score.Score);
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
            if (taskInfo.UserId != tokens.UserId)
                throw new TransactionException("96", "必须负责人编辑计划");
            iTaskAccess.GetTaskStep(request.TaskId);
            var taskStep = new TbUserTaskStep(){
                StepId = request.StepId,
                OrgId = tokens.OrgId,
                BranchId = tokens.BranchId,
                TaskId = request.TaskId,
                UserId = tokens.UserId,
                Content = request.StepContent,
                CreateTime = DateTime.Now
            };
            var result = iTaskAccess.EditTaskStep(request.Action, taskStep);
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
            var result = iTaskAccess.EditTaskFeedBack(request.TaskId, request.Content, request.AttachList);
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

        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            var response = new GetTaskStepResponse();
            if (request.AppId <= 0)
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            if (util.CheckAppId(request.AppId))
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
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var userId = request.UserId;
            var result = iTaskAccess.GetTask(userId);
            response.TaskList = result?.Select(x => new Application.Model.Common.Task
            {
                TaskId = x.TaskId,
                TaskeName = x.TaskName,
                TaskStatus = x.Status,
                TaskDate = x.BeginTime,
                TaskFromUser = x.FromUser,
                TaskToUser = x.UserId
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
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
                throw new TransactionException(MessageCode.InvalidToken, MessageKey.InvalidToken);
            var result = iTaskAccess.GetTaskDetail(request.TaskId, request.AppId);
            if (result != null)
            {
                response.TaskName = result.TaskName;
                response.TaskContent = result.TaskContent;
                response.TaskStatus = result.Status;
                response.TaskBeginDate = result.BeginTime;
                response.TaskEndDate = result.EndTime;
                response.TaskFounder = result.FromUser; //加密返回前端
                response.InitiateUserId = result.FromUser;
                response.AcceptUserId = result.UserId;
                response.AcctachmentList = new List<Attachment>();
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach1 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach2 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach3 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach4 });
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
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iTaskAccess.GetPublicTask(request.AppId);
            response.TaskList = result?.Select(x => new Application.Model.Common.Task
            {
                TaskId = x.TaskId,
                TaskeName = x.TaskName,
                TaskFromUser = x.FromUser,
                TaskStatus = x.Status,
                TaskDate = x.BeginTime
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
            if (util.CheckAppId(request.AppId))
                throw new TransactionException(MessageCode.InvalidParameter, MessageKey.InvalidParameter);
            var result = iTaskAccess.GetPublicTaskDetail(request.TaskId, request.AppId);
            if (result != null)
            {
                response.TaskName = result.TaskName;
                response.TaskContent = result.TaskContent;
                response.TaskStatus = result.Status;
                response.TaskBeginDate = result.BeginTime;
                response.TaskEndDate = result.EndTime;
                response.TaskFounder = result.FromUser; //加密返回前端
                response.InitiateUserId = result.FromUser;
                response.AcceptUserId = result.UserId;
                response.AcctachmentList = new List<Attachment>();
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach1 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach2 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach3 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach4 });              
            }
            else
            {
                throw new TransactionException(MessageCode.NoData, MessageKey.NoData);
            }
            return response;
        }
    }
}