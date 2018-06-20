using Eagles.Base;
using Eagles.Application.Model.AppModel.Activity.GetActivity;
using Eagles.Application.Model.AppModel.Activity.CreateActivity;
using Eagles.Application.Model.AppModel.Activity.GetActivityDetail;
using Eagles.Application.Model.AppModel.Activity.GetActivityComment;
using Eagles.Application.Model.AppModel.Activity.EditActivityJoin;
using Eagles.Application.Model.AppModel.Activity.EditActivityReview;
using Eagles.Application.Model.AppModel.Activity.EditActivityComment;
using Eagles.Application.Model.AppModel.Activity.EditActivityComplete;
using Eagles.Application.Model.AppModel.Activity.EditActivityFeedBack;

namespace Eagles.Interface.Core.Activity
{
    public interface IActivityHandler : IInterfaceBase
    {
        CreateActivityResponse CreateActivity(CreateActivityRequest request);
        EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request);
        EditActivityReviewResponse EditActivityReview(EditActivityReviewRequest request);
        EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request);
        EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request);
        EditActivityCommentResponse EditActivityComment(EditActivityCommentRequest request);
        GetActivityResponse GetActivity(GetActivityRequest request);
        GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request);
        GetActivityCommentResponse GetActivityComment(GetActivityCommentRequest request);
    }
}