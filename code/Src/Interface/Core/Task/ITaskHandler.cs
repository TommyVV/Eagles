using Eagles.Base;
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
        EditTaskFeedBackResponse EditTaskFeedBack(EditTaskFeedBackRequest request);
        GetTaskResponse GetTask(GetTaskRequest request);
        GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request);
        GetTaskCommentResponse GetTaskComment(GetTaskCommentRequest request);
        GetTaskStepResponse GetTaskStep(GetTaskStepRequest request);
    }
}