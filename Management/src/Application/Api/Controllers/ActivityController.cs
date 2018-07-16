using System.Web.Http;
using Eagles.Application.Host.Common;
using Eagles.Application.Model;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.ActivityTask.Response;
using Eagles.Base;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 活动（投票，在线考试）
    /// </summary>
    public class ActivityController : ApiController
    {
        private readonly IActivityTaskHandler _ActivityTaskHandler;

        public ActivityController(IActivityTaskHandler testHandler)
        {
            this._ActivityTaskHandler = testHandler;
        }


        /// <summary>
        /// 编辑 活动
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> EditActivity(EditActivityTaskInfoRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ActivityTaskHandler.EditActivity(requset));
        }

        /// <summary>
        /// 删除 活动
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<bool> RemoveActivity(RemoveActivityTaskRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ActivityTaskHandler.RemoveActivity(requset));
        }

        /// <summary>
        /// 活动 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetActivityTaskDetailResponse> GetActivityDetail(GetActivityTaskDetailRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ActivityTaskHandler.GetActivityDetail(requset));
        }

        /// <summary>
        /// 活动 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetActivityTaskResponse> GetActivity(GetActivityTaskRequset requset)
        {
            return ApiActuator.Runing(requset, (requset1) => _ActivityTaskHandler.GetActivity(requset1));

        }
    }
}