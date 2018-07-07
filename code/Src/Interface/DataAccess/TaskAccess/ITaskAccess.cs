﻿using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.DomainService.Model.Task;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.TaskAccess
{
    public interface ITaskAccess : IInterfaceBase
    {
        int CreateTask(TbTask reqTask, int toUserId);

        int RemoveTaskStep(int taskId, int stepId);

        int EditTaskAccept(TaskAcceptType type, int taskId, int reviewType);

        bool EditTaskComplete(int taskId, int isPublic, int completeStatus);

        int EditTaskComment(int orgId, int taskId, int userId, string content);
        
        int EditTaskStep(ActionEnum action, TbUserTaskStep taslStep);

        int EditTaskFeedBack(int taskId, string content, List<Attachment> list);
        
        List<TbUserTaskStep> GetTaskStep(int taskId);

        TbUserTaskStep GetStep(int stepId);

        List<TbTask> GetTask(int userId, string status);

        TbTask GetTaskDetail(int taskId, int appId);
        
        List<TbTask> GetPublicTask(int appId);

        TbTask GetPublicTaskDetail(int taskId, int appId);
    }
}