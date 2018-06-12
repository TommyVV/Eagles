using Eagles.Base;
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

namespace Eagles.Interface.Core.Task
{
    public interface ITaskHandler : IInterfaceBase
    {
        CreateTaskResponse CreateTask(CreateTaskRequest request);
        RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request);
        EditTaskStepResponse EditTaskStep(EditTaskStepRequest request);
        EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request);
        EditTaskCommentResponse EditTaskComment(EditTaskCommentRequest request);
        EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request);
        EditTaslFeedBackResponse EditTaslFeedBack(EditTaslFeedBackRequest request);
        GetTaskResponse GetTask(GetTaskRequest request);
        GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request);
        GetTaskCommentResponse GetTaskComment(GetTaskCommentRequest request);
        GetTaskStepResponse GetTaskStep(GetTaskStepRequest request);
    }
}