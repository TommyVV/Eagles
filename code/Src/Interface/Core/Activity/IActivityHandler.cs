using Eagles.Base;
using Eagles.Application.Model.AppModel.Activity.GetActivity;
using Eagles.Application.Model.AppModel.Activity.CreateActivity;
using Eagles.Application.Model.AppModel.Activity.GetActivityDetail;
using Eagles.Application.Model.AppModel.Activity.GetActivityComment;
using Eagles.Application.Model.AppModel.Activity.EditActivityJoin;
using Eagles.Application.Model.AppModel.Activity.EditActivityComment;
using Eagles.Application.Model.AppModel.Activity.EditActivityComplete;
using Eagles.Application.Model.AppModel.Activity.EditActivityFeedBack;

namespace Eagles.Interface.Core.Activity
{
    public interface IActivityHandler : IInterfaceBase
    {
        CreateActivityResponse CreateActivity(CreateActivityRequest request);
        GetActivityResponse GetActivity(GetActivityRequest request);
        GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request);
        GetActivityCommentResponse GetActivityComment(GetActivityCommentRequest request);

        EditActivityCommentResponse EditActivityComment(EditActivityCommentRequest request);
        EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request);
        EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request);
        EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request);
    }
}