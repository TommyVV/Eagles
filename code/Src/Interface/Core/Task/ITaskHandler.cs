﻿using Eagles.Base;
using Eagles.Application.Model.Task.CreateTask;
using Eagles.Application.Model.Task.EditTaskAccept;
using Eagles.Application.Model.Task.EditTaskComplete;
using Eagles.Application.Model.Task.EditTaskFeedBack;
using Eagles.Application.Model.Task.EditTaskStep;
using Eagles.Application.Model.Task.GetPublicTask;
using Eagles.Application.Model.Task.GetPublicTaskDetail;
using Eagles.Application.Model.Task.GetTask;
using Eagles.Application.Model.Task.GetTaskDetail;
using Eagles.Application.Model.Task.GetTaskStep;
using Eagles.Application.Model.Task.RemoveTaskStep;

namespace Eagles.Interface.Core.Task
{
    public interface ITaskHandler : IInterfaceBase
    {
        CreateTaskResponse CreateTask(CreateTaskRequest request);
        RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request);
        EditTaskStepResponse EditTaskStep(EditTaskStepRequest request);
        EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request);
        EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request);
        EditTaskFeedBackResponse EditTaskFeedBack(EditTaskFeedBackRequest request);
        GetTaskResponse GetTask(GetTaskRequest request);
        GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request);
        GetTaskStepResponse GetTaskStep(GetTaskStepRequest request);
        GetPublicTaskResponse GetPublicTask(GetPublicTaskRequest request);
        GetPublicTaskDetailResponse GetPublicTaskDetail(GetPublicTaskDetailRequest request);
    }
}