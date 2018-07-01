using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.DomainService.Model.Activity;

namespace Eagles.Interface.DataAccess.ActivityAccess
{
    public interface IActivityAccess : IInterfaceBase
    {
        int CreateActivity(DomainService.Model.Activity.TbActivity activity);

        int EditActivityJoin(int orgId, int branchId, int activityId, int userId);

        int EditActivityReview(ActivityTypeEnum type, int activityId);

        bool EditActivityComplete(int activityId);
        
        int EditActivityFeedBack(int activityyId, string content, List<Attachment> list);
        
        List<TbActivity> GetActivity(ActivityType activityType, ActivityPage activityPage, string userId = null);

        TbActivity GetActivityDetail(int activityId);

        List<TbActivity> GetPublicActivity(ActivityType activityType, int appId);
    }
}