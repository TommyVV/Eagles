using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Enums;
using Eagles.DomainService.Model.Task;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.TaskAccess
{
    public interface ITaskAccess : IInterfaceBase
    {
        int CreateTask(TbTask reqTask, int toUserId, string toUserName);

        int RemoveTaskStep(int taskId, int stepId);

        int EditTaskAccept(TaskAcceptType type, int taskId, int reviewType);

        bool EditTaskComplete(int taskId, int isPublic, int completeStatus, int score);
        
        int EditTaskStep(ActionEnum action, TbUserTaskStep taslStep);

        int EditTaskFeedBack(TbUserTaskStep userTaskStep);
        
        List<TbUserTaskStep> GetTaskStep(int taskId);

        List<TbTask> GetTask(int userId, TaskEnum taskType, int pageIndex, int pageSize);

        TbTask GetTaskDetail(int taskId, int appId);
        
        List<TbTask> GetPublicTask(int appId, int pageIndex, int pageSize);

        TbTask GetPublicTaskDetail(int taskId, int appId);
        
        TbUserTaskStep GetStepExist(int stepId);
    }
}