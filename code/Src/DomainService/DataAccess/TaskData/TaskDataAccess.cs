using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.Application.Model.Enums;
using Eagles.DomainService.Model.Task;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.TaskAccess;

namespace Ealges.DomianService.DataAccess.TaskData
{
    public class TaskDataAccess : ITaskAccess
    {
        private readonly IDbManager dbManager;

        public TaskDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public int CreateTask(TbTask reqTask, int toUserId, string toUserName)
        {
            var result = 0;
            var taskId = dbManager.ExecuteScalar<int>(@"insert into eagles.tb_task (OrgId,BranchId,TaskName,FromUser,FromUserName,TaskContent,BeginTime,EndTime,
AttachName1,AttachName2,AttachName3,AttachName4,Attach1,Attach2,Attach3,Attach4,CreateTime,CanComment,Status,IsPublic,OrgReview,BranchReview,CreateType) 
value (@OrgId,@BranchId,@TaskName,@FromUser,@FromUserName,@TaskContent,@BeginTime,@EndTime,@AttachName1,@AttachName2,@AttachName3,@AttachName4,@Attach1,@Attach2,@Attach3,@Attach4,
@CreateTime,@CanComment,@Status,@IsPublic,@OrgReview,@BranchReview,@CreateType); select LAST_INSERT_ID(); ", reqTask);
            result = dbManager.Excuted(@"insert into eagles.tb_user_task(OrgId,BranchId,TaskId,UserId,UserName) value (@OrgId,@BranchId,@TaskId,@UserId,@UserName) ",
                new
                {
                    OrgId = reqTask.OrgId,
                    BranchId = reqTask.BranchId,
                    TaskId = taskId,
                    UserId = toUserId, //任务责任人
                    UserName = toUserName
                });
            return result;
        }

        public int RemoveTaskStep(int taskId, int stepId)
        {
            return dbManager.Excuted("delete from eagles.tb_user_task_step where TaskId = @TaskId and StepId = @StepId", new { TaskId = taskId, StepId = stepId });
        }

        public int EditTaskAccept(TaskAcceptType type, int taskId, int reviewType)
        {
            var result = 0;
            switch (type)
            {
                case TaskAcceptType.Audit:
                    //上级审核任务
                    if (0 == reviewType)
                        result = dbManager.Excuted("update eagles.tb_task set Status = 0 where TaskId = @TaskId and Status = -1 ", new { TaskId = taskId }); //3-审核通过
                    else
                        result = dbManager.Excuted("update eagles.tb_task set Status = -9 where TaskId = @TaskId and Status = -1 ", new { TaskId = taskId }); //3-审核不通过
                    break;
                case TaskAcceptType.Accept:
                    //下级接受任务
                    result = dbManager.Excuted("update eagles.tb_task set Status = 0 where TaskId = @TaskId and Status = -2 ", new { TaskId = taskId }); //0-任务已接受
                    break;
                case TaskAcceptType.Apply:
                    //下级申请完成任务
                    result = dbManager.Excuted("update eagles.tb_task set Status = 2 where TaskId = @TaskId and Status = 0 ", new { TaskId = taskId }); //2-完成任务待审核
                    break;
            }
            return result;
        }

        public bool EditTaskComplete(int taskId, int isPublic, int completeStatus, int rewardScore, int score)
        {
            var commandString = "";
            if (completeStatus == 0)
                commandString = @"update eagles.tb_task set Status = 3, Score = @Score, IsPublic = @IsPublic where TaskId = @TaskId and Status = 2 "; //通过
            else
                commandString = @"update eagles.tb_task set Status = 0, IsPublic = @IsPublic where TaskId = @TaskId and Status = 2"; //不通过
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = commandString,
                    Parameter = new { IsPublic = isPublic, TaskId = taskId, Score = score }
                },
                new TransactionCommand()
                {
                    CommandString = "update tb_user_task set RewardsScore=@RewardsScore where TaskId=@TaskId",
                    Parameter = new { RewardsScore = rewardScore, TaskId = taskId }
                },
            };
            return dbManager.ExcutedByTransaction(commands);
        }
        
        public int EditTaskStep(ActionEnum action, TbUserTaskStep taslStep)
        {
            int result = 0;
            switch (action)
            {
                case ActionEnum.Create:
                    result = dbManager.Excuted(@"insert into eagles.tb_user_task_step (OrgId,BranchId,TaskId,UserId,StepName,Content,CreateTime) 
value (@OrgId,@BranchId,@TaskId,@UserId,@StepName,@Content,@CreateTime) ", taslStep);
                    break;
                case ActionEnum.Modify:
                    result = dbManager.Excuted("update eagles.tb_user_task_step set StepName = @StepName where TaskId = @TaskId and StepId = @StepId", taslStep);
                    break;
            }
            return result;
        }
        
        public int EditTaskFeedBack(TbUserTaskStep userTaskStep)
        {
            return dbManager.Excuted(@"update eagles.tb_user_task_step set Content = @Content, UpdateTime = @UpdateTime, 
AttachName1 = @AttachName1, AttachName2 = @AttachName2, AttachName3 = @AttachName3, AttachName4 = @AttachName4, 
Attach1 = @Attach1, Attach2 = @Attach2, Attach3 = @Attach3, Attach4 = @Attach4 
where TaskId = @TaskId and StepId = @StepId", userTaskStep);
        }

        public List<TbUserTaskStep> GetTaskStep(int taskId)
        {
            return dbManager.Query<TbUserTaskStep>(@"select a.OrgId,a.BranchId,a.TaskId,a.UserId,b.Name,a.StepId,a.StepName,a.CreateTime,a.Content,a.UpdateTime from eagles.tb_user_task_step a
join eagles.tb_user_info b on a.UserId = b.UserId where a.TaskId = @taskId",
                new { TaskId = taskId });
        }

        public List<TbTask> GetTask(int userId, TaskEnum taskType, int pageIndex, int pageSize)
        {
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            if (TaskEnum.From == taskType)
                return dbManager.Query<TbTask>(@"select a.TaskId,a.TaskName,a.TaskContent,a.FromUser,a.FromUserName,a.BeginTime,a.Status,b.UserId,b.UserName from eagles.tb_task a 
join eagles.tb_user_task b on a.TaskId = b.TaskId where a.FromUser = @UserId and a.Status <> -9 limit @PageIndex, @PageSize ",
                    new {UserId = userId, PageIndex = pageIndexParameter, PageSize = pageSize});
            else
                return dbManager.Query<TbTask>(@"select a.TaskId,a.TaskName,a.TaskContent,a.FromUser,a.FromUserName,a.BeginTime,a.Status,b.UserId,b.UserName from eagles.tb_task a 
join eagles.tb_user_task b on a.TaskId = b.TaskId where b.UserId = @UserId and a.Status <> -9 limit @PageIndex, @PageSize ",
                    new {UserId = userId, PageIndex = pageIndexParameter, PageSize = pageSize});
        }

        public TbTask GetTaskDetail(int taskId, int appId)
        {
            var result = dbManager.Query<TbTask>(@"select a.TaskId,a.TaskName,a.FromUser,a.FromUserName,a.Status,a.TaskContent,a.BeginTime,a.EndTime,a.AttachName1,a.AttachName2,a.AttachName3,a.AttachName4,
a.Attach1,a.Attach2,a.Attach3,a.Attach4,a.CreateTime,a.CreateType,b.UserId,UserName from eagles.tb_task a join eagles.tb_user_task b on a.taskId = b.taskId
where a.TaskId = @TaskId and a.OrgId = @OrgId ", new {TaskId = taskId, Orgid = appId });
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }
        
        public List<TbTask> GetPublicTask(int appId, int pageIndex, int pageSize)
        {
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            return dbManager.Query<TbTask>(@"select a.TaskId,a.TaskName,a.TaskContent,a.FromUser,a.BeginTime,a.Status from eagles.tb_task a 
join eagles.tb_user_task b on a.TaskId = b.TaskId where a.OrgId = @OrgId and a.IsPublic = @IsPublic and a.OrgReview = @OrgReview and a.BranchReview = @BranchReview 
limit @PageIndex, @PageSize ",
                new { Orgid = appId, IsPublic = 0, OrgReview = 0, BranchReview = 0, PageIndex = pageIndexParameter, PageSize = pageSize });
        }

        public TbTask GetPublicTaskDetail(int taskId, int appId)
        {
            var result = dbManager.Query<TbTask>(
                @"select a.TaskId,a.TaskName,a.FromUser,a.Status,a.TaskContent,a.AttachName1,a.AttachName2,a.AttachName3,a.AttachName4,a.Attach1,a.Attach2,a.Attach3,a.Attach4,
a.CreateTime,a.CreateType,b.UserId from eagles.tb_task a join eagles.tb_user_task b on a.taskId = b.taskId where a.TaskId = @TaskId and a.OrgId = @OrgId and a.IsPublic = @IsPublic 
and a.OrgReview = @OrgReview and a.BranchReview = @BranchReview ", new {TaskId = taskId, Orgid = appId, IsPublic = 0, OrgReview = 0, BranchReview = 0});
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }

        public TbUserTaskStep GetStepExist(int stepId)
        {
            return dbManager.QuerySingle<TbUserTaskStep>("select OrgId,BranchId,TaskId,UserId,StepId,StepName,CreateTime,Content,UpdateTime from eagles.tb_user_task_step where StepId = @StepId",
                new { StepId = stepId });
        }
    }
}