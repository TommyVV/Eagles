﻿using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.Task;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.Core.DataBase.TaskAccess;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.AppModel.Task.CreateTask;
using Eagles.Application.Model.AppModel.Task.EditTaskAccept;
using Eagles.Application.Model.AppModel.Task.EditTaskComment;
using Eagles.Application.Model.AppModel.Task.EditTaskComplete;
using Eagles.Application.Model.AppModel.Task.EditTaskStep;
using Eagles.Application.Model.AppModel.Task.EditTaskFeedBack;
using Eagles.Application.Model.AppModel.Task.GetTask;
using Eagles.Application.Model.AppModel.Task.GetTaskComment;
using Eagles.Application.Model.AppModel.Task.GetTaskDetail;
using Eagles.Application.Model.AppModel.Task.GetTaskStep;
using Eagles.Application.Model.AppModel.Task.RemoveTaskStep;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.Task
{
    public class TaskHandler : ITaskHandler
    {
        private readonly IDesEncrypt desEncrypt;
        private readonly ITaskAccess iTaskAccess;
        private readonly IUtil util;

        public TaskHandler(ITaskAccess iTaskAccess, IUtil util, IDesEncrypt desEncrypt)
        {
            this.iTaskAccess = iTaskAccess;
            this.util = util;
            this.desEncrypt = desEncrypt;
        }

        public CreateTaskResponse CreateTask(CreateTaskRequest request)
        {
            var response = new CreateTaskResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("01", "用户不存在");
            }
            var task = new DomainModel.Task.TbTask();
            task.TaskName = request.TaskName;
            task.BeginTime = request.TaskBeginDate;
            task.EndTime = request.TaskEndDate;
            task.TaskContent = request.TaskContent;
            task.FromUser = request.TaskFromUser;
            task.CanComment = request.CanComment;
            task.IsPublic = request.IsPublic;
            if (0 == userInfo.IsLeader)
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
            var result = iTaskAccess.CreateTask(tokens.OrgId, tokens.BranchId, request.TaskToUserId, task);
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

        public RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request)
        {
            var response = new RemoveTaskStepResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.RemoveTaskStep(request.TaskId, request.StepId);
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

        public EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request)
        {
            var response = new EditTaskAcceptResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.EditTaskAccept(request.Type, request.TaskId);
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

        public EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request)
        {
            var response = new EditTaskCompleteResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.EditTaskComplete(request.TaskId, request.IsPublic);
            if (result)
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

        public EditTaskCommentResponse EditTaskComment(EditTaskCommentRequest request)
        {
            var response = new EditTaskCommentResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.EditTaskComment(tokens.OrgId, request.TaskId, request.CommentUserId, request.Comment);
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

        public EditTaskStepResponse EditTaskStep(EditTaskStepRequest request)
        {
            var response = new EditTaskStepResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.EditTaskStep(request.Action, tokens.OrgId, tokens.BranchId, tokens.UserId,
                request.StepContent, request.TaskId.ToString(), request.StepId.ToString());
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

        public EditTaskFeedBackResponse EditTaskFeedBack(EditTaskFeedBackRequest request)
        {
            var response = new EditTaskFeedBackResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.EditTaskFeedBack(request.TaskId, request.Content, request.AttachList);
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

        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            var response = new GetTaskResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userId = desEncrypt.Decrypt(request.EncryptUserid);
            var result = iTaskAccess.GetTask(userId);
            response.TaskList = result?.Select(x => new Application.Model.Common.Task
            {
                TaskId = x.TaskId,
                TaskeName = x.TaskName,
                TaskFromUser = x.FromUser,
                TaskStatus = x.Status,
                TaskDate = x.BeginTime
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

        public GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request)
        {
            var response = new GetTaskDetailResponse();
            var tokens = util.GetUserId(request.Token, 0);
            if (tokens == null || tokens.UserId <= 0)
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.GetTaskDetail(request.TaskId);
            if (result != null)
            {
                response.TaskName = result.TaskName;
                response.TaskContent = result.TaskContent;
                response.TaskStatus = result.Status;
                response.TaskBeginDate = result.BeginTime;
                response.TaskEndDate = result.EndTime;
                response.TaskFounder = result.FromUser;
                response.AcctachmentList = new List<Attachment>();
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach1 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach2 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach3 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach4 });
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

        public GetTaskCommentResponse GetTaskComment(GetTaskCommentRequest request)
        {
            var response = new GetTaskCommentResponse();
            var result = iTaskAccess.GetTaskComment(request.TaskId);
            response.TaskCommentList = result?.Select(x => new Comment
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
        
        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            var response = new GetTaskStepResponse();
            var result = iTaskAccess.GetTaskStep(request.TaskId);
            response.StepList = result?.Select(x => new Step
            {
                StepId = x.StepId,
                StepName = x.StepName
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