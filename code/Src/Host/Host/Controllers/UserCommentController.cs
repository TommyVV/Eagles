using System.Web.Http;
using Eagles.Application.Model.Activity.EditActivityComment;
using Eagles.Application.Model.Activity.GetActivityComment;
using Eagles.Interface.Core.Activity;

namespace Eagles.Application.Host.Controllers
{
    public class UserCommentController: ApiController
    {
        private IActivityHandler activityHandler;

        public UserCommentController(IActivityHandler activityHandler)
        {
            this.activityHandler = activityHandler;
        }

        /// <summary>
        /// 用户评论查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetActivityCommentResponse GetActivityComment(GetActivityCommentRequest request)
        {
            return activityHandler.GetActivityComment(request);
        }

        /// <summary>
        /// 用户评论接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public EditActivityCommentResponse EditActivityComment(EditActivityCommentRequest request)
        {
            return activityHandler.EditActivityComment(request);
        }
    }
}