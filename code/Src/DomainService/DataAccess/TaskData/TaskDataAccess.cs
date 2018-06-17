using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Curd.Enum;
using Eagles.Interface.Core.DataBase.TaskAccess;
using DomainModel = Eagles.DomainService.Model;

namespace Ealges.DomianService.DataAccess.TaskData
{
    public class TaskDataAccess : ITaskAccess
    {
        private readonly IDbManager dbManager;

        public TaskDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int CreateTask(DomainModel.Task.Task reqTask)
        {
            return dbManager.Excuted(@"insert into eagles.tb_task (TaskName,FromUser,TaskContent,BeginTime,EndTime,AttachType1,AttachType2,AttachType3,AttachType4,
Attach1,Attach2,Attach3,Attach4,CreateTime,CanComment,Status,IsPublic,OrgReview,BranchReview) 
value (@TaskName,@FromUser,@TaskContent,@BeginTime,@EndTime,@AttachType1,@AttachType2,@AttachType3,@AttachType4,@Attach1,@Attach2,@Attach3,@Attach4,@CreateTime,@CanComment,@Status,@IsPublic,@OrgReview,@BranchReview) ",
                new
                {
                    TaskName = reqTask.TaskName,
                    FromUser = reqTask.FromUser,
                    TaskContent = reqTask.TaskContent,
                    BeginTime = reqTask.BeginTime,
                    EndTime = reqTask.EndTime,
                    AttachType1 = reqTask.AttachType1,
                    AttachType2 = reqTask.AttachType2,
                    AttachType3 = reqTask.AttachType3,
                    AttachType4 = reqTask.AttachType4,
                    Attach1 = reqTask.Attach1,
                    Attach2 = reqTask.Attach2,
                    Attach3 = reqTask.Attach3,
                    Attach4 = reqTask.Attach4,
                    CreateTime = reqTask.CreateTime,
                    CanComment = reqTask.CanComment,
                    Status = -1,
                    IsPublic = reqTask.IsPublic,
                    OrgReview = "-1",
                    BranchReview = "-1"
                });
        }

        public int RemoveTaskStep(int taskId)
        {
            return dbManager.Excuted("delete eagles.tb_user_task_step where TaskId = @TaskId ", new {TaskId = taskId});
        }

        public int EditTaskAccept(int taskId)
        {
            return dbManager.Excuted("update eagles.tb_task set Status = '1' where TaskId = @TaskId ",new {TaskId = taskId});
        }

        public int EditTaskComplete(int taskId, int isPublic)
        {
            return dbManager.Excuted("update eagles.tb_task set Status = '2', IsPublic = @IsPublic where TaskId = @TaskId ", new {IsPublic = isPublic, TaskId = taskId});
        }

        public int EditTaskComment(int taskId, int userId, string content)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(Id,Content,Createtime,UserId,ReviewStatus) value (@Id,@Content,@Createtime,@UserId,@ReviewStatus)",
                new
                {
                    Id = taskId,
                    Content = content,
                    UserId = userId,
                    ReviewStatus = "-1"
                });
        }

        public int EditTaskStep(ActionEnum action, string content, string stepId = null)
        {
            int result = 0;
            if (action == ActionEnum.Create)
            {
                //todo 新增
            }
            else if (action == ActionEnum.Modify)
            {
                //todo 修改
            }
            return result;
        }
        
        public int EditTaskFeedBack(int taskId, string content, List<Attachment> list)
        {
            //todo 步骤附件
            return dbManager.Excuted("update eagles.tb_user_task_step set Content = @Content where TaskId = @TaskId ", new {Content = content, TaskId = taskId});
        }

        public List<DomainModel.Task.Task> GetTask()
        {
            return dbManager.Query<DomainModel.Task.Task>("select taskId,taskName,TaskContent,FromUser,BeginTime from eagles.tb_task ", new { });
        }

        public DomainModel.Task.Task GetTaskDetail(int taskId)
        {
            var result = dbManager.Query<DomainModel.Task.Task>(@"select TaskId,TaskName,FromUser,Status,TaskContent,AttachType1
,AttachType2,AttachType3,AttachType4,Attach1,Attach2,Attach3,Attach4,CreateTime from eagles.tb_task 
where TaskId = @TaskId", new {TaskId = taskId});
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public List<DomainModel.User.UserComment> GetTaskComment(int taskId)
        {
            return dbManager.Query<DomainModel.User.UserComment>("select Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment where Id = @Id", new {Id = taskId});
        }

        public List<DomainModel.User.UserTaskStep> GetTaskStep(int taskId)
        {
            return dbManager.Query<DomainModel.User.UserTaskStep>("select OrgId,BranchId,TaskId,UserId,StepId,StepName,CreateTime,Content,UpdateTime from eagles.tb_user_task_step where TaskId = @taskId", new {TaskId = taskId});
        }
    }
}