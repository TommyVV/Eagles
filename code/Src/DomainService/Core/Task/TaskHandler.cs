using System.Linq;
using Eagles.Interface.Core.Task;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Curd.Task.CreateTask;
using Eagles.Application.Model.Curd.Task.EditTaskAccept;
using Eagles.Application.Model.Curd.Task.EditTaskComment;
using Eagles.Application.Model.Curd.Task.EditTaskComplete;
using Eagles.Application.Model.Curd.Task.EditTaskStep;
using Eagles.Application.Model.Curd.Task.EditTaskFeedBack;
using Eagles.Application.Model.Curd.Task.GetTask;
using Eagles.Application.Model.Curd.Task.GetTaskComment;
using Eagles.Application.Model.Curd.Task.GetTaskDetail;
using Eagles.Application.Model.Curd.Task.GetTaskStep;
using Eagles.Application.Model.Curd.Task.RemoveTaskStep;
using Eagles.Interface.Core.DataBase.TaskAccess;
using Eagles.Interface.DataAccess.Util;
using DomainModel = Eagles.DomainService.Model;

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
            {
                response.ErrorCode = "96";
                response.Message = "获取Token失败";
                return response;
            }
            var act = new DomainModel.Task.Task();
            act.TaskName = request.TaskName;
            act.BeginTime = request.TaskBeginDate;
            act.EndTime = request.TaskEndDate;
            act.TaskContent = request.TaskContent;
            act.FromUser = request.TaskFromUser;
            act.CanComment = request.CanComment;
            act.IsPublic = request.IsPublic;
            var list = request.AttachList;
            if (request.AttachList != null && request.AttachList.Count > 0)
            {
                act.Attach1 = list[0].AttachmentDownloadUrl is null ? null : list[0].AttachmentDownloadUrl;
                act.Attach2 = list[1].AttachmentDownloadUrl is null ? null : list[1].AttachmentDownloadUrl;
                act.Attach3 = list[2].AttachmentDownloadUrl is null ? null : list[2].AttachmentDownloadUrl;
                act.Attach4 = list[3].AttachmentDownloadUrl is null ? null : list[3].AttachmentDownloadUrl;
            }
            var result = iTaskAccess.CreateTask(act);
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
            var result = iTaskAccess.RemoveTaskStep(request.TaskId);
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
            var result = iTaskAccess.EditTaskAccept(request.TaskId);
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
            var result = iTaskAccess.EditTaskComment(request.TaskId, request.CommentUserId, request.Comment);
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
            //todo
            var result = iTaskAccess.EditTaskStep(request.Action, request.StepContent, request.StepId.ToString());
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
            var result = iTaskAccess.GetTask();
            response.TaskList = result?.Select(x => new Application.Model.Common.Task
            {
                TaskId = x.TaskId,
                TaskeName = x.TaskName,
                TaskFromUser = x.FromUser,
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