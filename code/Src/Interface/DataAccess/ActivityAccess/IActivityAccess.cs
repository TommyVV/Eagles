using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Activity;

namespace Eagles.Interface.DataAccess.ActivityAccess
{
    public interface IActivityAccess : IInterfaceBase
    {
        int CreateActivity(TbActivity activity);

        int EditActivityJoin(TbUserActivity userActivity);

        int EditActivityReview(ActivityTypeEnum type, int activityId, int reviewType);

        bool EditActivityComplete(int activityId, int completeStatus);
        
        int EditActivityFeedBack(TbUserActivity userActivity);

        List<TbUserActivity> GetActivityFeedBack(int activityId, int appId);

        List<TbActivity> GetActivity(ActivityType activityType,int branchId, int pageIndex, int pageSize);
        
        TbActivity GetActivityDetail(int activityId, int appId);

        List<TbActivity> GetPublicActivity(ActivityType activityType, int appId, int pageIndex, int pageSize);

        TbActivity GetPublicActivityDetail(int activityId, int appId);

        List<JoinPeople> GetActivityJoinPeople(int activityId);
        
        int GetUserActivityCount(int activityId);

        List<TbUserActivity> GetUserActivity(int userId);
    }
}