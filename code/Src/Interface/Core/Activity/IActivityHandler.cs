using Eagles.Base;
using Eagles.Application.Model.Activity.CreateActivity;
using Eagles.Application.Model.Activity.EditActivityJoin;
using Eagles.Application.Model.Activity.EditActivityReview;
using Eagles.Application.Model.Activity.EditActivityComplete;
using Eagles.Application.Model.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Activity.GetActivity;
using Eagles.Application.Model.Activity.GetActivityDetail;
using Eagles.Application.Model.Activity.GetPublicActivity;
using Eagles.Application.Model.Activity.GetPublicActivityDetail;


namespace Eagles.Interface.Core.Activity
{
    public interface IActivityHandler : IInterfaceBase
    {
        CreateActivityResponse CreateActivity(CreateActivityRequest request);
        EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request);
        EditActivityReviewResponse EditActivityReview(EditActivityReviewRequest request);
        EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request);
        EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request);
        GetActivityResponse GetActivity(GetActivityRequest request);
        GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request);
        GetPublicActivityResponse GetPublicActivity(GetPublicActivityRequest request);
        GetPublicActivityDetailResponse GetPublicActivityDetail(GetPublicActivityDetailRequest request);
    }
}