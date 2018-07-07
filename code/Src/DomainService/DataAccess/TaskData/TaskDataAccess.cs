using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
using Eagles.Application.Model.Common;
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

        public int CreateTask(TbTask reqTask, int toUserId)
        {
            var result = 0;
            var taskId = dbManager.ExecuteScalar<int>(@"insert into eagles.tb_task (OrgId,BranchId,TaskName,FromUser,TaskContent,BeginTime,EndTime,AttachType1,AttachType2,AttachType3,AttachType4,
Attach1,Attach2,Attach3,Attach4,CreateTime,CanComment,Status,IsPublic,OrgReview,BranchReview,CreateType) 
value (@OrgId,@BranchId,@TaskName,@FromUser,@TaskContent,@BeginTime,@EndTime,@AttachType1,@AttachType2,@AttachType3,@AttachType4,@Attach1,@Attach2,@Attach3,@Attach4,
@CreateTime,@CanComment,@Status,@IsPublic,@OrgReview,@BranchReview,@CreateType); select LAST_INSERT_ID(); ", reqTask);
            result = dbManager.Excuted(@"insert into eagles.tb_user_task(OrgId,BranchId,TaskId,UserId) value (@OrgId,@BranchId,@TaskId,@UserId) ",
                new
                {
                    OrgId = reqTask.OrgId,
                    BranchId = reqTask.BranchId,
                    TaskId = taskId,
                    UserId = toUserId //任务责任人
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

        public bool EditTaskComplete(int taskId, int isPublic, int completeStatus)
        {
            //查询任务奖励积分
            var score = dbManager.ExecuteScalar<int>("select Score from eagles.tb_reward_score where RewardType = 0", new { });
            var commandString = "";
            if (completeStatus == 0)
                commandString = @"update eagles.tb_task set Status = 3, IsPublic = @IsPublic where TaskId = @TaskId and Status = 2 "; //通过
            else
                commandString = @"update eagles.tb_task set Status = -8, IsPublic = @IsPublic where TaskId = @TaskId and Status = 2"; //不通过
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = commandString,
                    Parameter = new { IsPublic = isPublic, TaskId = taskId }
                },
                new TransactionCommand()
                {
                    CommandString = "update tb_user_task set RewardsScore=@RewardsScore where TaskId=@TaskId",
                    Parameter = new { RewardsScore = score, TaskId = taskId }
                },
            };
            return dbManager.ExcutedByTransaction(commands);
        }

        public int EditTaskComment(int orgId, int taskId, int userId, string content)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_comment(OrgId,Id,Content,Createtime,UserId,ReviewStatus) value (@OrgId,@Id,@Content,@Createtime,@UserId,@ReviewStatus)",
                new
                {
                    OrgId = orgId,
                    Id = taskId,
                    Content = content,
                    Createtime = DateTime.Now,
                    UserId = userId,
                    ReviewStatus = "-1"
                });
        }

        public int EditTaskStep(ActionEnum action, TbUserTaskStep taslStep)
        {
            int result = 0;
            switch (action)
            {
                case ActionEnum.Create:
                    result = dbManager.Excuted(@"insert into eagles.tb_user_task_step (OrgId,BranchId,TaskId,UserId,StepName,Content,CreateTime) value (@OrgId,@BranchId,@TaskId,@UserId,@StepName,@Content,@CreateTime) ", taslStep);
                    break;
                case ActionEnum.Modify:
                    result = dbManager.Excuted("update eagles.tb_user_task_step set StepName = @StepName where TaskId = @TaskId and StepId = @StepId", taslStep);
                    break;
            }
            return result;
        }
        
        public int EditTaskFeedBack(int taskId, string content, List<Attachment> attachList)
        {
            string attach1 = string.Empty, attach2 = string.Empty, attach3 = string.Empty, attach4 = string.Empty,
                attachType1 = string.Empty, attachType2 = string.Empty, attachType3 = string.Empty, attachType4 = string.Empty;
            for (int i = 0; i < attachList.Count; i++)
            {
                if (i == 0)
                {
                    attachType1 = attachList[i].AttachmentType;
                    attach1 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 1)
                {
                    attachType2 = attachList[i].AttachmentType;
                    attach2 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 2)
                {
                    attachType3 = attachList[i].AttachmentType;
                    attach3 = attachList[i].AttachmentDownloadUrl;
                }
                else if (i == 3)
                {
                    attachType4 = attachList[i].AttachmentType;
                    attach4 = attachList[i].AttachmentDownloadUrl;
                }
            }
            return dbManager.Excuted(@"update eagles.tb_user_task_step set Content = @Content, UpdateTime = @UpdateTime, 
AttachType1 = @AttachType1, AttachType2 = @AttachType2, AttachType3 = @AttachType3, AttachType4 = @AttachType4, Attach1 = @Attach1, Attach2 = @Attach2, Attach3 = @Attach3, Attach4 = @Attach4 
where TaskId = @TaskId ",
                new
                {
                    Content = content,
                    UpdateTime = DateTime.Now,
                    AttachType1 = attachType1,
                    AttachType2 = attachType2,
                    AttachType3 = attachType3,
                    AttachType4 = attachType4,
                    Attach1 = attach1,
                    Attach2 = attach2,
                    Attach3 = attach3,
                    Attach4 = attach4,
                    TaskId = taskId
                });
        }

        public List<TbUserTaskStep> GetTaskStep(int taskId)
        {
            return dbManager.Query<TbUserTaskStep>("select OrgId,BranchId,TaskId,UserId,StepId,StepName,CreateTime,Content,UpdateTime from eagles.tb_user_task_step where TaskId = @taskId",
                new { TaskId = taskId });
        }

        public TbUserTaskStep GetStep(int stepId)
        {
            return dbManager.QuerySingle<TbUserTaskStep>("select OrgId,BranchId,TaskId,UserId,StepId,StepName,CreateTime,Content,UpdateTime from eagles.tb_user_task_step where StepId = @StepId",
                new { StepId = stepId });
        }

        public List<TbTask> GetTask(int userId, string status)
        {
            if (string.IsNullOrEmpty(status))
                return dbManager.Query<TbTask>(@"select a.TaskId,a.TaskName,a.TaskContent,a.FromUser,a.BeginTime,a.Status,b.UserId from eagles.tb_task a 
join eagles.tb_user_task b on a.TaskId = b.TaskId where b.UserId = @UserId ", new { UserId = userId });
            else
                return dbManager.Query<TbTask>(@"select a.TaskId,a.TaskName,a.TaskContent,a.FromUser,a.BeginTime,a.Status,b.UserId from eagles.tb_task a 
join eagles.tb_user_task b on a.TaskId = b.TaskId where b.UserId = @UserId, a.Status = @Status ", new { UserId = userId, Status = status });
        }

        public TbTask GetTaskDetail(int taskId, int appId)
        {
            var result = dbManager.Query<TbTask>(
                @"select a.TaskId,a.TaskName,a.FromUser,a.Status,a.TaskContent,a.AttachType1,a.AttachType2,a.AttachType3,a.AttachType4,a.Attach1,a.Attach2,a.Attach3,a.Attach4,
a.CreateTime,a.CreateType, b.UserId from eagles.tb_task a join eagles.tb_user_task b on a.taskId = b.taskId where a.TaskId = @TaskId and a.OrgId = @OrgId ", new {TaskId = taskId, Orgid = appId });
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }
        
        public List<TbTask> GetPublicTask(int appId)
        {
            return dbManager.Query<TbTask>(@"select a.TaskId,a.TaskName,a.TaskContent,a.FromUser,a.BeginTime,a.Status from eagles.tb_task a 
join eagles.tb_user_task b on a.TaskId = b.TaskId where a.OrgId = @OrgId and a.IsPublic = @IsPublic and a.OrgReview = @OrgReview and a.BranchReview = @BranchReview ",
                new {Orgid = appId, IsPublic = 0, OrgReview = 0, BranchReview = 0});
        }

        public TbTask GetPublicTaskDetail(int taskId, int appId)
        {
            var result = dbManager.Query<TbTask>(
                @"select a.TaskId,a.TaskName,a.FromUser,a.Status,a.TaskContent,a.AttachType1,a.AttachType2,a.AttachType3,a.AttachType4,a.Attach1,a.Attach2,a.Attach3,a.Attach4,
a.CreateTime,a.CreateType,b.UserId from eagles.tb_task a join eagles.tb_user_task b on a.taskId = b.taskId where a.TaskId = @TaskId and a.OrgId = @OrgId and a.IsPublic = @IsPublic 
and a.OrgReview = @OrgReview and a.BranchReview = @BranchReview ", new {TaskId = taskId, Orgid = appId, IsPublic = 0, OrgReview = 0, BranchReview = 0});
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }
    }
}