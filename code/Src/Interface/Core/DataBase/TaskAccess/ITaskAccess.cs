using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Curd.Enum;

namespace Eagles.Interface.Core.DataBase.TaskAccess
{
    public interface ITaskAccess : IInterfaceBase
    {
        int CreateTask(DomainService.Model.Task.Task task);

        int RemoveTaskStep(int taskId);

        int EditTaskAccept(int taskId);

        int EditTaskComplete(int taskId, int isPublic);

        int EditTaskComment(int taskId, int userId, string content);
        
        int EditTaskStep(ActionEnum action, string content, string stepId = null);

        int EditTaskFeedBack(int taskId, string content, List<Attachment> list);
        
        List<Eagles.DomainService.Model.Task.Task> GetTask();

        DomainService.Model.Task.Task GetTaskDetail(int taskId);

        List<DomainService.Model.User.UserComment> GetTaskComment(int taskId);

        List<DomainService.Model.User.UserTaskStep> GetTaskStep(int taskId);
        
    }
}