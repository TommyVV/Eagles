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

        int EditActivityJoin(int orgId, int branchId, int activityId, int userId);

        int EditActivityReview(ActivityTypeEnum type, int activityId, int reviewType);

        bool EditActivityComplete(int activityId);
        
        int EditActivityFeedBack(int activityyId, string content, List<Attachment> list);
        
        List<TbActivity> GetActivity(ActivityType activityType,int branchId);
        
        List<TbUserActivity> GetUserActivity(int userId);

        TbActivity GetActivityDetail(int activityId, int appId);

        List<TbActivity> GetPublicActivity(ActivityType activityType, int appId);

        TbActivity GetPublicActivityDetail(int activityId, int appId);

        List<JoinPeople> GetActivityJoinPeople(int activityId);

        List<TbUserActivity> GetActivityFeedBack(int activityId, int appId);
    }
}