using System.Web.Http;
using Eagles.Application.Model;
using Eagles.Interface.Core.Activity;
using Eagles.Application.Model.Activity.CreateActivity;
using Eagles.Application.Model.Activity.EditActivityJoin;
using Eagles.Application.Model.Activity.EditActivityReview;
using Eagles.Application.Model.Activity.EditActivityComplete;
using Eagles.Application.Model.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Activity.GetActivity;
using Eagles.Application.Model.Activity.GetActivityDetail;
using Eagles.Base;

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
        public ResponseFormat<GetActivityResponse> GetActivityList(GetActivityRequest request)
        {
            return  ApiActuator.Runing(() => activityHandler.GetActivity(request));
        }

        /// <summary>
        /// 活动详情查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ResponseFormat<GetActivityDetailResponse> GetActivityDetail(GetActivityDetailRequest request)
        {
         //   ApiActuator.Runing(() => activityHandler.GetActivity(request));
            return ApiActuator.Runing(() => activityHandler.GetActivityDetail(request));
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
    }
}