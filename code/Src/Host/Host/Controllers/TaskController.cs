using System.Web.Http;
using Eagles.Application.Model.Curd.Task.GetTask;
using Eagles.Interface.Core.Activity;

namespace Eagles.Host.Controllers
{
    /// <summary>
    /// 任务Cotrollers
    /// </summary>
    public class TaskController : ApiController
    {
        private ITaskHandler taskHandler;

        /// <summary>
        /// 任务查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetActivity")]
        [HttpGet]
        public GetTaskResponse GetTask(GetTaskRequest request)
        {
            return taskHandler.GetTask(request);
        }
    }
}