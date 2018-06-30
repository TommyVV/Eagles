using System.Web.Http;
using Eagles.Application.Model.Task.CreateTask;
using Eagles.Application.Model.Task.EditTaskAccept;
using Eagles.Application.Model.Task.EditTaskComplete;
using Eagles.Application.Model.Task.EditTaskFeedBack;
using Eagles.Application.Model.Task.EditTaskStep;
using Eagles.Application.Model.Task.GetTask;
using Eagles.Application.Model.Task.GetTaskDetail;
using Eagles.Application.Model.Task.GetTaskStep;
using Eagles.Application.Model.Task.RemoveTaskStep;
using Eagles.Interface.Core.Task;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 任务Cotrollers
    /// </summary>
    [ValidServiceToken]
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
        [HttpPost]
        public CreateTaskResponse CreateTask(CreateTaskRequest request)
        {
            return taskHandler.CreateTask(request);
        }

        /// <summary>
        /// 任务步骤删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        [HttpPost]
        public EditTaskFeedBackResponse EditTaskFeedBack(EditTaskFeedBackRequest request)
        {
            return taskHandler.EditTaskFeedBack(request);
        }
        
        /// <summary>
        /// 任务查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        [HttpPost]
        public GetTaskStepResponse GetTaskStep(GetTaskStepRequest request)
        {
            return taskHandler.GetTaskStep(request);
        }
    }
}