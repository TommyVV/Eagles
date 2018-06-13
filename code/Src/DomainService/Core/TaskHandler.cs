using System;
using Eagles.Interface.Core.Task;
using Eagles.Application.Model.Curd.Task.CreateTask;
using Eagles.Application.Model.Curd.Task.EditTaskAccept;
using Eagles.Application.Model.Curd.Task.EditTaskComment;
using Eagles.Application.Model.Curd.Task.EditTaskComplete;
using Eagles.Application.Model.Curd.Task.EditTaskStep;
using Eagles.Application.Model.Curd.Task.EditTaslFeedBack;
using Eagles.Application.Model.Curd.Task.GetTask;
using Eagles.Application.Model.Curd.Task.GetTaskComment;
using Eagles.Application.Model.Curd.Task.GetTaskDetail;
using Eagles.Application.Model.Curd.Task.GetTaskStep;
using Eagles.Application.Model.Curd.Task.RemoveTaskStep;

namespace Eagles.DomainService.Core
{
    public class TaskHandler : ITaskHandler
    {
        public CreateTaskResponse CreateTask(CreateTaskRequest request)
        {
            return new CreateTaskResponse()
            {
                ErrorCode = "00",
                Message = "创建成功"
            };
        }

        public EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaskCommentResponse EditTaskComment(EditTaskCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaskStepResponse EditTaskStep(EditTaskStepRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaslFeedBackResponse EditTaslFeedBack(EditTaslFeedBackRequest request)
        {
            throw new NotImplementedException();
        }

        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            throw new NotImplementedException();
        }

        public GetTaskCommentResponse GetTaskComment(GetTaskCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request)
        {
            throw new NotImplementedException();
        }

        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            throw new NotImplementedException();
        }

        public RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request)
        {
            throw new NotImplementedException();
        }
    }
}