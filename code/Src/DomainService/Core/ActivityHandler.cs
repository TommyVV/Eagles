﻿using System;
using Eagles.Application.Model.Curd.Activity.EditActivityComment;
using Eagles.Application.Model.Curd.Activity.EditActivityComplete;
using Eagles.Application.Model.Curd.Activity.EditActivityFeedBack;
using Eagles.Application.Model.Curd.Activity.EditActivityJoin;
using Eagles.Application.Model.Curd.Activity.GetActivity;
using Eagles.Application.Model.Curd.Activity.CreateActivity;
using Eagles.Application.Model.Curd.Activity.GetActivityComment;
using Eagles.Application.Model.Curd.Activity.GetActivityDetail;
using Eagles.Interface.Core.Activity;

namespace Eagles.DomainService.Core
{
    public class ActivityHandler : IActivityHandler
    {
        public CreateActivityResponse CreateActivity(CreateActivityRequest request)
        {

            return new CreateActivityResponse()
            {
                ErrorCode = "00",
                Message = "创建成功"
            };
        }

        public EditActivityCommentResponse EditActivityComment(EditActivityCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public EditActivityCompleteResponse EditActivityComplete(EditActivityCompleteRequest request)
        {
            throw new NotImplementedException();
        }

        public EditActivityFeedBackResponse EditActivityFeedBack(EditActivityFeedBackRequest request)
        {
            throw new NotImplementedException();
        }

        public EditActivityJoinResponse EditActivityJoin(EditActivityJoinRequest request)
        {
            throw new NotImplementedException();
        }

        public GetActivityResponse GetActivity(GetActivityRequest request)
        {
            throw new NotImplementedException();
        }

        public GetActivityCommentResponse GetActivityComment(GetActivityCommentRequest request)
        {
            throw new NotImplementedException();
        }

        public GetActivityDetailResponse GetActivityDetail(GetActivityDetailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}