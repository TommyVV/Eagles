using System.Collections.Generic;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Enums;
using Eagles.Base;

namespace Eagles.Interface.DataAccess.TaskAccess
{
    public interface ITaskAccess : IInterfaceBase
    {
        int CreateTask(int orgId, int branchId, int toUserId, DomainService.Model.Task.TbTask task);

        int RemoveTaskStep(int taskId, int stepId);

        int EditTaskAccept(TaskTypeEnum type, int taskId);

        bool EditTaskComplete(int taskId, int isPublic);

        int EditTaskComment(int orgId, int taskId, int userId, string content);
        
        int EditTaskStep(ActionEnum action, int orgId, int branchId, int userId, string content, string taskId = null, string stepId = null);

        int EditTaskFeedBack(int taskId, string content, List<Attachment> list);
        
        List<Eagles.DomainService.Model.Task.TbTask> GetTask(string userId);

        DomainService.Model.Task.TbTask GetTaskDetail(int taskId);

        List<DomainService.Model.User.TbUserComment> GetTaskComment(int taskId);

        List<DomainService.Model.User.TbUserTaskStep> GetTaskStep(int taskId);
        
    }
}