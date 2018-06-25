using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Base.DesEncrypt;
using Eagles.Interface.Core.Task;
using Eagles.Interface.DataAccess.Util;
using Eagles.Interface.DataAccess.TaskAccess;
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
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var userInfo = util.GetUserInfo(tokens.UserId);
            if (userInfo == null)
            {
                throw new TransactionException("01", "用户不存在");
            }
            var fromUser = Convert.ToInt32(desEncrypt.Decrypt(request.TaskFromUser)); //任务发起人
            var toUser = Convert.ToInt32(desEncrypt.Decrypt(request.TaskToUserId)); //任务负责人
            var task = new DomainModel.Task.TbTask();
            task.TaskName = request.TaskName;
            task.BeginTime = request.TaskBeginDate;
            task.EndTime = request.TaskEndDate;
            task.TaskContent = request.TaskContent;
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
            var result = iTaskAccess.CreateTask(tokens.OrgId, tokens.BranchId, toUser, task);
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
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
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
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            
            var taskInfo = iTaskAccess.GetUserTask(request.TaskId);
            if (taskInfo == null)
            {
                response.Code = "96";
                response.Message = "任务不存在";
                return response;
            }
            switch (request.Type)
            {
                case TaskTypeEnum.Audit:
                    //上级审核任务
                    //TODO:是否是上级
                    break;
                case TaskTypeEnum.Accept:
                    //下级接受任务
                    if (taskInfo.UserId != tokens.UserId)
                    {
                        response.Code = "96";
                        response.Message = "必须负责人申请完成任务";
                        return response;
                    }
                    break;
                case TaskTypeEnum.Apply:
                    //下级申请完成任务
                    if (taskInfo.UserId != tokens.UserId)
                    {
                        response.Code = "96";
                        response.Message = "必须负责人申请完成任务";
                        return response;
                    }
                    break;
            }
            var result = iTaskAccess.EditTaskAccept(request.Type, request.TaskId);
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
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
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
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var result = iTaskAccess.EditTaskStep(request.Action, tokens.OrgId, tokens.BranchId, tokens.UserId,
                request.StepContent, request.TaskId.ToString(), request.StepId.ToString());
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
            {
                response.Code = "96";
                response.Message = "获取Token失败";
                return response;
            }
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

        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            var response = new GetTaskResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
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

        public GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request)
        {
            var response = new GetTaskDetailResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = iTaskAccess.GetTaskDetail(request.TaskId);
            if (result != null)
            {
                response.TaskName = result.TaskName;
                response.TaskContent = result.TaskContent;
                response.TaskStatus = result.Status;
                response.TaskBeginDate = result.BeginTime;
                response.TaskEndDate = result.EndTime;
                response.TaskFounder = desEncrypt.Encrypt(result.FromUser.ToString()); //加密返回前端
                response.AcctachmentList = new List<Attachment>();
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach1 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach2 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach3 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result.Attach4 });
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
        
        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            var response = new GetTaskStepResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var result = iTaskAccess.GetTaskStep(request.TaskId);
            response.StepList = result?.Select(x => new Step
            {
                StepId = x.StepId,
                StepName = x.StepName
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

    }
}