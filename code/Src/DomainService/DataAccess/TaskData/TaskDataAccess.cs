using System.Collections.Generic;
using System.Net.Mime;
using Eagles.Base.DataBase;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Curd.Enum;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core.DataBase.TaskAccess;
using Eagles.DomainService.Model.Task;
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

        public int CreateTask(Eagles.DomainService.Model.Task.Task reqTask)
        {
            return dbManager.Excuted(@"insert into eagles.tb_task (TaskName,FromUser,TaskContent,BeginTime,EndTime,Attch1,Attch2,Attch3,Attch4,CreateTime,CanComment,Status,IsPublic,OrgReview,BranchReview) 
value (@TaskName,@FromUser,@TaskContent,@BeginTime,@EndTime,@Attch1,@Attch2,@Attch3,@Attch4,@CreateTime,@CanComment,@Status,@IsPublic,@OrgReview,@BranchReview) ",
                new
                {
                    TaskName = new[] {reqTask.TaskName},
                    FromUser = new[] {reqTask.FromUser},
                    TaskContent = new[] {reqTask.TaskContent},
                    BeginTime = new[] {reqTask.BeginTime},
                    EndTime = new[] {reqTask.EndTime},
                    Attach1 = new[] {reqTask.Attach1},
                    Attach2 = new[] {reqTask.Attach2},
                    Attach3 = new[] {reqTask.Attach3},
                    Attach4 = new[] {reqTask.Attach4},
                    CreateTime = new[] {reqTask.CreateTime},
                    CanComment = new[] {reqTask.CanComment},
                    Status = new[] {"-1"},
                    IsPublic = new[] {reqTask.IsPublic},
                    OrgReview = new[] {"-1"},
                    BranchReview = new[] {"-1"}
                });
        }

        public int RemoveTaskStep(int taskId)
        {
            return dbManager.Excuted("delete eagles.tb_user_task_step where TaskId = @TaskId ", new { TaskId = new[] { taskId } });
        }

        public int EditTaskAccept(int taskId)
        {
            return dbManager.Excuted("update eagles.tb_task set Status = '1' where TaskId = @TaskId ", new { TaskId = new[] { taskId } });
        }

        public int EditTaskComplete(int taskId, int isPublic)
        {
            return dbManager.Excuted("update eagles.tb_task set Status = '2', IsPublic = @IsPublic where TaskId = @TaskId ", new { IsPublic = new[] { isPublic } ,TaskId = new[] { taskId } });
        }

        public int EditTaskComment(int taskId, int userId, string content)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(Id,Content,Createtime,UserId,ReviewStatus) value (@Id,@Content,@Createtime,@UserId,@ReviewStatus)",
                new
                {
                    Id = new[] { taskId },
                    Content = new[] { content },
                    UserId = new[] { userId },
                    ReviewStatus = new[] { "-1" }
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
            return dbManager.Excuted("update eagles.tb_user_task_step set Content = @Content where TaskId = @TaskId ", new { Content = new[] { content }, TaskId = new[] {taskId}});
        }

        public List<Eagles.DomainService.Model.Task.Task> GetTask()
        {
            return dbManager.Query<DomainModel.Task.Task>("select taskId,taskName,ImageUrl,HtmlContent from eagles.TB_Task", null);
        }

        public Eagles.DomainService.Model.Task.Task GetTaskDetail(int taskId)
        {
            var result = dbManager.Query<DomainModel.Task.Task>(
                "select TaskId,TaskName,FromUser,Status,TaskContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,Attach2,Attach3,Attach4,CreateTime from eagles.tb_task where TaskId = @taskId",
                new { TaskId = new[] { taskId } });
            return result[0];
        }

        public List<UserComment> GetTaskComment(int taskId)
        {
            return dbManager.Query<UserComment>("select Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment where Id = @Id", new { Id = new[] { taskId } });
        }

        public List<UserTaskStep> GetTaskStep(int taskId)
        {
            return dbManager.Query<UserTaskStep>("select OrgId,BranchId,TaskId,UserId,StepId,StepName,CreateTime,Content,UpdateTime from eagles.tb_user_task_step where TaskId = @taskId",
                new { TaskId = new[] { taskId } });
        }
    }
}