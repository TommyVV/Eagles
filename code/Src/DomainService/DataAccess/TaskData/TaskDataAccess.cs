using System;
using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
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

        public int CreateTask(int orgId, int branchId, int toUserId, DomainModel.Task.Task reqTask)
        {
            #region 事务操作taskId无法获取
            /*
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = @"insert into eagles.tb_task (TaskName,FromUser,TaskContent,BeginTime,EndTime,AttachType1,AttachType2,AttachType3,AttachType4,
                    Attach1,Attach2,Attach3,Attach4,CreateTime,CanComment,Status,IsPublic,OrgReview,BranchReview) 
                    value(@TaskName, @FromUser, @TaskContent, @BeginTime, @EndTime, @AttachType1, @AttachType2, @AttachType3, @AttachType4, @Attach1, @Attach2, @Attach3, @Attach4,
                    @CreateTime, @CanComment, @Status, @IsPublic, @OrgReview, @BranchReview)",
                    Parameter =  new {TaskName = reqTask.TaskName, FromUser = reqTask.FromUser, TaskContent = reqTask.TaskContent, BeginTime = reqTask.BeginTime,
                        EndTime = reqTask.EndTime, AttachType1 = reqTask.AttachType1, AttachType2 = reqTask.AttachType2, AttachType3 = reqTask.AttachType3,
                        AttachType4 = reqTask.AttachType4, Attach1 = reqTask.Attach1, Attach2 = reqTask.Attach2, Attach3 = reqTask.Attach3, Attach4 = reqTask.Attach4,
                        CreateTime = reqTask.CreateTime, CanComment = reqTask.CanComment, Status = 0, IsPublic = reqTask.IsPublic, OrgReview = "-1", BranchReview = "-1"
                    }
                },
                //todo taskId不明确
                new TransactionCommand()
                {
                    CommandString = "insert into eagles.tb_user_task(TaskId,UserId) value (insert into eagles.tb_user_task(@TaskId,@UserId)",
                    Parameter =  new { TaskId = "", UserId = reqTask.FromUser }
                }
            };
            dbManager.ExcutedByTransaction(commands);
            */
            #endregion

            var result = 0;
            result = dbManager.Excuted(@"insert into eagles.tb_task (OrgId,BranchId,TaskName,FromUser,TaskContent,BeginTime,EndTime,AttachType1,AttachType2,AttachType3,AttachType4,
Attach1,Attach2,Attach3,Attach4,CreateTime,CanComment,Status,IsPublic,OrgReview,BranchReview,ToUserId) 
value (@OrgId,@BranchId,@TaskName,@FromUser,@TaskContent,@BeginTime,@EndTime,@AttachType1,@AttachType2,@AttachType3,@AttachType4,@Attach1,@Attach2,@Attach3,@Attach4,
@CreateTime,@CanComment,@Status,@IsPublic,@OrgReview,@BranchReview,@ToUserId) ",
                new
                {
                    OrgId = orgId,
                    BranchId = branchId,
                    TaskName = reqTask.TaskName,
                    FromUser = reqTask.FromUser, //任务发起人
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
                    CreateTime = DateTime.Now,
                    CanComment = reqTask.CanComment,
                    Status = reqTask.Status, //任务状态初始状态
                    IsPublic = reqTask.IsPublic,
                    OrgReview = "-1",
                    BranchReview = "-1"
                });
            var taskId = dbManager.ExecuteScalar("select LAST_INSERT_ID()", null);
            result = dbManager.Excuted(@"insert into eagles.tb_user_task(OrgId,BranchId,TaskId,UserId) value (@OrgId,@BranchId,@TaskId,@UserId) ",
                new
                {
                    OrgId = orgId,
                    BranchId = branchId,
                    TaskId = taskId,
                    UserId = toUserId //任务责任人
                });
            return result;
        }

        public int RemoveTaskStep(int taskId)
        {
            return dbManager.Excuted("delete eagles.tb_user_task_step where TaskId = @TaskId ", new {TaskId = taskId});
        }

        public int EditTaskAccept(TaskTypeEnum type, int taskId)
        {
            var result = 0;
            switch (type)
            {
                case TaskTypeEnum.Audit:
                    result = dbManager.Excuted("update eagles.tb_task set Status = 3 where TaskId = @TaskId ", new { TaskId = taskId });
                    break;
                case TaskTypeEnum.Accept:
                    result = dbManager.Excuted("update eagles.tb_task set Status = 1 where TaskId = @TaskId ", new { TaskId = taskId });
                    break;
                case TaskTypeEnum.Apply:
                    result = dbManager.Excuted("update eagles.tb_task set Status = 2 where TaskId = @TaskId ", new { TaskId = taskId });
                    break;
            }
            return result;
        }

        public bool EditTaskComplete(int taskId, int isPublic)
        {
            //查询任务奖励积分
            var score = dbManager.ExecuteScalar("select Score from eagles.TB_REWARD_SCORE where RewardType = 0", new { });
            var commands = new List<TransactionCommand>()
            {
                new TransactionCommand()
                {
                    CommandString = "update eagles.tb_task set Status = 4, IsPublic = @IsPublic where TaskId = @TaskId",
                    Parameter =  new {IsPublic = isPublic, TaskId = taskId}
                },
                new TransactionCommand()
                {
                    CommandString = "update tb_user_task set RewardsScore=@RewardsScore where TaskId=@TaskId",
                    Parameter =  new {RewardsScore = score, TaskId = taskId}
                },
            };
            return dbManager.ExcutedByTransaction(commands);
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

        public int EditTaskStep(ActionEnum action, int orgId, int branchId, int userId, string content, string taskId = null, string stepId = null)
        {
            int result = 0;
            switch (action)
            {
                case ActionEnum.Create:
                    result = dbManager.Excuted(@"insert into eagles.tb_user_task_step (OrgId,BranchId,TaskId,UserId,StepName,CreateTime) 
value (@OrgId,@BranchId,@TaskId,@UserId,@StepName,@CreateTime) ",
                        new
                        {
                            OrgId = orgId,
                            BranchId = branchId,
                            TaskId = taskId,
                            UserId = userId,
                            StepName = content,
                            CreateTime = DateTime.Now
                        });
                    break;
                case ActionEnum.Modify:
                    result = dbManager.Excuted("update eagles.tb_user_task_step set StepName = @StepName where TaskId = @TaskId and StepId = @StepId",
                        new
                        {
                            StepName = content,
                            TaskId = taskId,
                            StepId = stepId
                        });
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

        public List<DomainModel.Task.Task> GetTask(string userId)
        {
            return dbManager.Query<DomainModel.Task.Task>(@"select a.TaskId,a.TaskName,a.TaskContent,a.FromUser,a.BeginTime,a.Status from eagles.tb_task a 
join eagles.tb_user_task b on a.TaskId = b.TaskId where b.UserId = @UserId ", new {UserId = userId});
        }

        public DomainModel.Task.Task GetTaskDetail(int taskId)
        {
            var result = dbManager.Query<DomainModel.Task.Task>(@"select a.TaskId,a.TaskName,a.FromUser,a.Status,a.TaskContent,a.AttachType1,a.AttachType2,a.AttachType3,a.AttachType4,
a.Attach1,a.Attach2,a.Attach3,a.Attach4,a.CreateTime from eagles.tb_task a join eagles.tb_user_task b on a.taskId = b.taskId where a.TaskId = @TaskId", new {TaskId = taskId});
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