using System.Web.Http;
using Eagles.Application.Model.Curd.Activity.CreateActivity;
using Eagles.Application.Model.Curd.Activity.EditActivityComment;
using Eagles.Application.Model.Curd.Activity.EditActivityComplete;
using Eagles.Application.Model.Curd.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Curd.Activity.EditActivityJoin;
using Eagles.Application.Model.Curd.Activity.GetActivity;
using Eagles.Application.Model.Curd.Activity.GetActivityComment;
using Eagles.Application.Model.Curd.Activity.GetActivityDetail;
using Eagles.Interface.Core.Activity;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 活动Cotroller
    /// </summary>
    public class ActivityController : ApiController
    {
        private IActivityHandler activityHandler;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="activityHandler"></param>
        public ActivityController(IActivityHandler activityHandler)
        {
            this.activityHandler = activityHandler;
        }

        /// <summary>
        /// 活动查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetActivity")]
        [HttpPost]
        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            return activityHandler.GetActivity(request);
        }

        /// <summary>
        /// 活动详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetActivityDetail")]
        [HttpPost]
        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            return activityHandler.GetActivityDetail(request);
        }

        /// <summary>
        /// 活动回复查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/GetActivityComment")]
        [HttpPost]
        public GetActivityCommentResponse GetActivityComment(GetActivityCommentRequest request)
        {
            return activityHandler.GetActivityComment(request);
        }

        /// <summary>
        /// 活动发布
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/CreateActivity")]
        [HttpPost]
        public CreateActivityResponse CreateActivity(CreateActivityRequest request)
        {
            return activityHandler.CreateActivity(request);
        }

        /// <summary>
        /// 活动评论接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditActivityComment")]
        [HttpPost]
        public EditActivityCommentResponse EditActivityComment(EditActivityCommentRequest request)
        {
            return activityHandler.EditActivityComment(request);
        }

        /// <summary>
        /// 活动完成接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditActivityComplete")]
        [HttpPost]
        public EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request)
        {
            return activityHandler.EditActivityComplete(request);
        }

        /// <summary>
        /// 活动反馈接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditActivityFeedBack")]
        [HttpPost]
        public EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            return activityHandler.EditActivityFeedBack(request);
        }

        /// <summary>
        /// 活动参与接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Route("api/EditActivityJoin")]
        [HttpPost]
        public EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request)
        {
            return activityHandler.EditActivityJoin(request);
        }
    }
}