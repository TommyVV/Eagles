using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Application.Model.Activity.CreateActivity;
using Eagles.Application.Model.Activity.EditActivityComment;
using Eagles.Application.Model.Activity.EditActivityComplete;
using Eagles.Application.Model.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Activity.EditActivityJoin;
using Eagles.Application.Model.Activity.GetActivity;
using Eagles.Application.Model.Activity.GetActivityComment;
using Eagles.Application.Model.Activity.GetActivityDetail;
using Eagles.Interface.Core.Activity;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 活动Cotroller
    /// </summary>
    [ValidServiceToken]
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
        [HttpPost]
        public GetActivityResponse GetActivityList(GetActivityRequest request)
        {
            return activityHandler.GetActivity(request);
        }

        /// <summary>
        /// 活动详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
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
        [HttpPost]
        public ResponseBase EditActivityComplete(EditActivityCompleteRequest request)
        {
            return activityHandler.EditActivityComplete(request);
        }

        /// <summary>
        /// 活动反馈接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseBase EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            return activityHandler.EditActivityFeedBack(request);
        }

        /// <summary>
        /// 活动参与接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request)
        {
            return activityHandler.EditActivityJoin(request);
        }
    }
}