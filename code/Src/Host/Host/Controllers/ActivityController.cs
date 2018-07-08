using System.Web.Http;
using Eagles.Base;
using Eagles.Interface.Core.Activity;
using Eagles.Application.Model.Activity.CreateActivity;
using Eagles.Application.Model.Activity.EditActivityJoin;
using Eagles.Application.Model.Activity.EditActivityReview;
using Eagles.Application.Model.Activity.EditActivityComplete;
using Eagles.Application.Model.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Activity.GetActivity;
using Eagles.Application.Model.Activity.GetActivityDetail;
using Eagles.Application.Model.Activity.GetActivityFeedBack;
using Eagles.Application.Model.Activity.GetPublicActivity;
using Eagles.Application.Model.Activity.GetPublicActivityDetail;

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
        /// 活动发布
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<CreateActivityResponse> CreateActivity(CreateActivityRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.CreateActivity(request));
        }

        /// <summary>
        /// 活动审核接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditActivityReviewResponse> EditActivityReview(EditActivityReviewRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.EditActivityReview(request));
        }

        /// <summary>
        /// 活动完成接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditActivityCompleteResponse> EditActivityComplete(EditActivityCompleteRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.EditActivityComplete(request));
        }

        /// <summary>
        /// 活动反馈接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditActivityFeedBackResponse> EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.EditActivityFeedBack(request));
        }

        /// <summary>
        /// 活动参与接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<EditActivityJoinResponse> EditActivityJoin(EditActivityJoinRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.EditActivityJoin(request));
        }

        /// <summary>
        /// 活动查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetActivityResponse> GetActivityList(GetActivityRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.GetActivity(request));
        }

        /// <summary>
        /// 活动详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetActivityDetailResponse> GetActivityDetail(GetActivityDetailRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.GetActivityDetail(request));
        }

        /// <summary>
        /// 公开活动查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetPublicActivityResponse> GetPublicActivityList(GetPublicActivityRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.GetPublicActivity(request));
        }

        /// <summary>
        /// 公开活动详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetPublicActivityDetailResponse> GetPublicActivityDetail(GetPublicActivityDetailRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.GetPublicActivityDetail(request));
        }

        /// <summary>
        /// 活动反馈查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetActivityFeedBackResponse> GetActivityFeedBack(GetActivityFeedBackRequest request)
        {
            return ApiActuator.Runing(() => activityHandler.GetActivityFeedBack(request));
        }
    }
}