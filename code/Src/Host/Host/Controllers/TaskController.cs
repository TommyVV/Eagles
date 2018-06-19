using System.Web.Http;
using Eagles.Interface.Core.Task;
using Eagles.Application.Model.Curd.Task.CreateTask;
using Eagles.Application.Model.Curd.Task.RemoveTaskStep;
using Eagles.Application.Model.Curd.Task.EditTaskStep;
using Eagles.Application.Model.Curd.Task.EditTaskAccept;
using Eagles.Application.Model.Curd.Task.EditTaskComment;
using Eagles.Application.Model.Curd.Task.EditTaskComplete;
using Eagles.Application.Model.Curd.Task.EditTaskFeedBack;
using Eagles.Application.Model.Curd.Task.GetTask;
using Eagles.Application.Model.Curd.Task.GetTaskDetail;
using Eagles.Application.Model.Curd.Task.GetTaskStep;
using Eagles.Application.Model.Curd.Task.GetTaskComment;

namespace Eagles.Host.Controllers
{
    /// <summary>
    /// 任务Cotrollers
    /// </summary>
    public class TaskController : ApiController
    {
        private ITaskHandler taskHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskHandler"></param>
        public TaskController(ITaskHandler taskHandler)
        {
            this.taskHandler = taskHandler;
        }

        /// <summary>
        /// 任务发布
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/CreateTask")]
        [HttpPost]
        public CreateTaskResponse CreateTask(CreateTaskRequest request)
        {
            return taskHandler.CreateTask(request);
        }

        /// <summary>
        /// 任务删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/RemoveTaskStep")]
        [HttpPost]
        public RemoveTaskStepResponse RemoveTaskStep(RemoveTaskStepRequest request)
        {
            return taskHandler.RemoveTaskStep(request);
        }

        /// <summary>
        /// 任务完成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditTaskComplete")]
        [HttpPost]
        public EditTaskCompleteResponse EditTaskComplete(EditTaskCompleteRequest request)
        {
            return taskHandler.EditTaskComplete(request);
        }

        /// <summary>
        /// 任务步骤编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditTaskStep")]
        [HttpPost]
        public EditTaskStepResponse EditTaskStep(EditTaskStepRequest request)
        {
            return taskHandler.EditTaskStep(request);
        }

        /// <summary>
        /// 任务完成接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditTaskAccept")]
        [HttpPost]
        public EditTaskAcceptResponse EditTaskAccept(EditTaskAcceptRequest request)
        {
            return taskHandler.EditTaskAccept(request);
        }

        /// <summary>
        /// 任务回馈接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditTaskFeedBack")]
        [HttpPost]
        public EditTaskFeedBackResponse EditTaskFeedBack(EditTaskFeedBackRequest request)
        {
            return taskHandler.EditTaskFeedBack(request);
        }

        /// <summary>
        /// 任务评论接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditTaskComment")]
        [HttpPost]
        public EditTaskCommentResponse EditTaskComment(EditTaskCommentRequest request)
        {
            return taskHandler.EditTaskComment(request);
        }
        
        /// <summary>
        /// 任务查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetTask")]
        [HttpPost]
        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            return taskHandler.GetTask(request);
        }

        /// <summary>
        /// 任务详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetTaskDetail")]
        [HttpPost]
        public GetTaskDetailResponse GetTaskDetail(GetTaskDetailRequest request)
        {
            return taskHandler.GetTaskDetail(request);
        }

        /// <summary>
        /// 任务步骤查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetTaskStep")]
        [HttpPost]
        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            return taskHandler.GetTaskStep(request);
        }

        /// <summary>
        /// 任务评论查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetTaskComment")]
        [HttpPost]
        public GetTaskCommentResponse GetTaskComment(GetTaskCommentRequest request)
        {
            return taskHandler.GetTaskComment(request);
        }
    }
}