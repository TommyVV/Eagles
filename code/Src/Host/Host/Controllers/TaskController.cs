using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.Task;
using Eagles.Application.Model.Task.CreateTask;
using Eagles.Application.Model.Task.RemoveTaskStep;
using Eagles.Application.Model.Task.EditTaskStep;
using Eagles.Application.Model.Task.EditTaskAccept;
using Eagles.Application.Model.Task.EditTaskComplete;
using Eagles.Application.Model.Task.EditTaskFeedBack;
using Eagles.Application.Model.Task.GetTaskStep;
using Eagles.Application.Model.Task.GetTask;
using Eagles.Application.Model.Task.GetTaskDetail;
using Eagles.Application.Model.Task.GetPublicTask;
using Eagles.Application.Model.Task.GetPublicTaskDetail;

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
        public ResponseFormat<CreateTaskResponse> CreateTask(CreateTaskRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.CreateTask(request));
        }

        /// <summary>
        /// 任务步骤删除
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<RemoveTaskStepResponse> RemoveTaskStep(RemoveTaskStepRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.RemoveTaskStep(request));
        }

        /// <summary>
        /// 任务完成
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditTaskCompleteResponse> EditTaskComplete(EditTaskCompleteRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.EditTaskComplete(request));
        }

        /// <summary>
        /// 任务步骤编辑
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditTaskStepResponse> EditTaskStep(EditTaskStepRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.EditTaskStep(request));
        }

        /// <summary>
        /// 任务完成接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditTaskAcceptResponse> EditTaskAccept(EditTaskAcceptRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.EditTaskAccept(request));
        }

        /// <summary>
        /// 任务回馈接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditTaskFeedBackResponse> EditTaskFeedBack(EditTaskFeedBackRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.EditTaskFeedBack(request));
        }

        /// <summary>
        /// 任务步骤查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetTaskStepResponse> GetTaskStep(GetTaskStepRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.GetTaskStep(request));
        }

        /// <summary>
        /// 任务查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetTaskResponse> GetTask(GetTaskRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.GetTask(request));
        }

        /// <summary>
        /// 任务详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetTaskDetailResponse> GetTaskDetail(GetTaskDetailRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.GetTaskDetail(request));
        }

        /// <summary>
        /// 公开任务查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetPublicTaskResponse> GetPublicTask(GetPublicTaskRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.GetPublicTask(request));
        }

        /// <summary>
        /// 公开任务详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetPublicTaskDetailResponse> GetPublicTaskDetail(GetPublicTaskDetailRequest request)
        {
            return ApiActuator.Runing(() => taskHandler.GetPublicTaskDetail(request));
        }
    }
}