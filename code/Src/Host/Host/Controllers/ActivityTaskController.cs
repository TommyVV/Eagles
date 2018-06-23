﻿using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.ActivityTask.Response;
using Eagles.Interface.Core;

namespace Eagles.Application.Host.Controllers
{
   
    public class ActivityTaskController : ApiController
    {
        private readonly IActivityTaskHandler _ActivityTaskHandler;

        public ActivityTaskController(IActivityTaskHandler testHandler)
        {
            this._ActivityTaskHandler = testHandler;
        }


        /// <summary>
        /// 编辑 活动
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/EditActivityTask")]
        [HttpPost]
        public ResponseBase EditActivityTask(EditActivityTaskInfoRequset requset)
        {
            return _ActivityTaskHandler.EditActivity(requset);
        }

        /// <summary>
        /// 删除 活动
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/RemoveActivityTask")]
        [HttpPost]
        public ResponseBase RemoveActivityTask(RemoveActivityTaskRequset requset)
        {
            return _ActivityTaskHandler.RemoveActivity(requset);
        }

        /// <summary>
        /// 活动 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetActivityTaskDetail")]
        [HttpPost]
        public GetActivityTaskDetailResponse GetActivityTaskDetail(GetActivityTaskDetailRequset requset)
        {
            return _ActivityTaskHandler.GetActivityDetail(requset);
        }

        /// <summary>
        /// 活动 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        [Route("api/GetActivityTask")]
        [HttpPost]
        public GetActivityTaskResponse GetActivityTask(GetActivityTaskRequset requset)
        {
            return _ActivityTaskHandler.GetActivity(requset);
        }
    }
}