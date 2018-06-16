using System;
using System.Linq;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.Task;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Curd.Task.CreateTask;
using Eagles.Application.Model.Curd.Task.EditTaskAccept;
using Eagles.Application.Model.Curd.Task.EditTaskComment;
using Eagles.Application.Model.Curd.Task.EditTaskComplete;
using Eagles.Application.Model.Curd.Task.EditTaskStep;
using Eagles.Application.Model.Curd.Task.EditTaslFeedBack;
using Eagles.Application.Model.Curd.Task.GetTask;
using Eagles.Application.Model.Curd.Task.GetTaskComment;
using Eagles.Application.Model.Curd.Task.GetTaskDetail;
using Eagles.Application.Model.Curd.Task.GetTaskStep;
using Eagles.Application.Model.Curd.Task.RemoveTaskStep;
using DomainModel = Eagles.DomainService.Model;

namespace Eagles.DomainService.Core.Task
{
    public class TaskHandler : ITaskHandler
    {
        private readonly IDbManager dbManager;

        public CreateTaskResponse CreateTask(CreateTaskRequest request)
        {
            var response = new CreateTaskResponse();
            //校验Token
            var token = request.Token;
            var attachType1 = "";
            var attachType2 = "";
            var attachType3 = "";
            var attachType4 = "";
            var attach1 = "";
            var attach2 = "";
            var attach3 = "";
            var attach4 = "";
            var taskType = request.TaskType;
            var taskName = request.TaskName;
            var beginTime = request.TaskBeginDate;
            var endTime = request.TaskName;
            var maxCount = "";
            var testId = "";
            var maxUser = "";
            var content = request.TaskContent;
            var fromUser = request.TaskFromUser;
            var canComment = request.CanComment;
            var isPublic = request.IsPublic;
            var list = request.AttachList;
            if (request.AttachList != null && request.AttachList.Count > 0)
            {
                attachType1 = list[0].AttachmentType is null ? null : list[0].AttachmentType;
                attachType2 = list[1].AttachmentType is null ? null : list[1].AttachmentType;
                attachType3 = list[2].AttachmentType is null ? null : list[2].AttachmentType;
                attachType4 = list[3].AttachmentType is null ? null : list[3].AttachmentType;
                attach1 = list[0].AttachmentDownloadUrl is null ? null : list[0].AttachmentDownloadUrl;
                attach2 = list[1].AttachmentDownloadUrl is null ? null : list[1].AttachmentDownloadUrl;
                attach3 = list[2].AttachmentDownloadUrl is null ? null : list[2].AttachmentDownloadUrl;
                attach4 = list[3].AttachmentDownloadUrl is null ? null : list[3].AttachmentDownloadUrl;
            }
            var result = dbManager.Excuted(@"insert into eagles.tb_task (TaskName,FromUser,TaskContent,BeginTime,endTime,Attch1,Attch2,Attch3,Attch4,CreateTime,CanComment,Status,IsPublic) 
value (@taskName, @HtmlContent, @BeginTime, @EndTime, @FromUser, @taskType, @MaxCount, @CanComment, @TestId, @MaxUser, @Attach1, @Attach2, @Attach3, @Attach4, @AttachType1, @AttachType2, @AttachType3, @AttachType4, @ImageUrl, @IsPublic, @OrgReview, @BranchReview)",
                new object[]
                {
                    taskName, content, beginTime, endTime, fromUser, taskType, maxCount, canComment, testId, maxUser,
                    attach1, attach2, attach3, attach4, attachType1, attachType2, attachType3, attachType4, isPublic, "-1", "-1"
                });
            if (result > 0)
            {
                response.ErrorCode = "00";
                response.Message = "成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "失败";
            }
            return response;
        }

        public RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaskCommentResponse EditTaskComment(EditTaskCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaskStepResponse EditTaskStep(EditTaskStepRequest request)
        {
            throw new NotImplementedException();
        }

        public EditTaslFeedBackResponse EditTaslFeedBack(EditTaslFeedBackRequest request)
        {
            throw new NotImplementedException();
        }

        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            var response = new GetTaskResponse();
            var userId = request.EncryptUserid;
            var result = dbManager.Query<DomainModel.Task.Task>("select taskId,taskName,ImageUrl,HtmlContent from eagles.TB_Task", null);
            response.TaskList = result?.Select(x => new Application.Model.Common.Task
            {
                TaskId = x.TaskId,
                TaskeName = x.TaskName,
                TaskFromUser = x.FromUser,
                TaskDate = x.BeginTime
            }).ToList();
            if (result != null && result.Count > 0)
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetTaskCommentResponse GetTaskComment(GetTaskCommentRequest request)
        {
            var response = new GetTaskCommentResponse();
            var taskId = request.TaskId;
            var result = dbManager.Query<DomainModel.User.UserComment>("select Id,OrgId,MessageId,Content,CreateTime,UserId,ReviewUser,ReviewTime from eagles.tb_user_comment where Id = @Id", taskId);
            response.TaskCommentList = result?.Select(x => new Comment
            {
                CommentId = x.MessageId,
                CommentTime = x.ReviewTime,
                CommentUserId = x.UserId,
                CommentContent = x.Content
            }).ToList();
            if (result != null && result.Count > 0)
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request)
        {
            var response = new GetTaskDetailResponse();
            var taskId = request.TaskId;
            //request.EncryptUserid;
            var result = dbManager.Query<DomainModel.Task.Task>("select TaskId,TaskName,FromUser,Status,TaskContent,AttachType1,AttachType2,AttachType3,AttachType4,Attach1,Attach2,Attach3,Attach4,CreateTime from eagles.TB_TASK where taskId = @id", taskId);
            if (result != null && result.Count > 0)
            {
                response.TaskName = result[0].TaskName;
                response.TaskContent = result[0].TaskContent;
                response.TaskStatus = result[0].Status;
                response.TaskBeginDate = result[0].BeginTime;
                response.TaskEndDate = result[0].endTime;
                response.TaskFounder = result[0].FromUser;
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result[0].Attach1 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result[0].Attach2 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result[0].Attach3 });
                response.AcctachmentList.Add(new Attachment() { AttachmentName = result[0].Attach4 });
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            var response = new GetTaskStepResponse();
            var taskId = request.TaskId;
            var result = dbManager.Query<DomainModel.User.UserTaskStep>("select OrgId,BranchId,TaskId,UserId,StepId,StepName,CreateTime,Content,UpdateTime FROM eagles.tb_user_task_step where taskId = @taskId", taskId);
            response.StepList = result?.Select(x => new Step
            {
                StepId = x.StepId,
                StepName = x.StepName
            }).ToList();
            if (result != null && result.Count > 0)
            {
                response.ErrorCode = "00";
                response.Message = "查询成功";
            }
            else
            {
                response.ErrorCode = "96";
                response.Message = "查无数据";
            }
            return response;
        }

    }
}