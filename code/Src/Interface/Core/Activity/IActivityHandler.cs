﻿using Eagles.Application.Model.Curd.Activity.CreateActivity;
using Eagles.Application.Model.Curd.Activity.EditActivityComment;
using Eagles.Application.Model.Curd.Activity.EditActivityComplete;
using Eagles.Base;
using Eagles.Application.Model.Curd.Activity.GetActivity;
using Eagles.Application.Model.Curd.Activity.GetActivityDetail;
using Eagles.Application.Model.Curd.Activity.GetActivityComment;

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
    }
}