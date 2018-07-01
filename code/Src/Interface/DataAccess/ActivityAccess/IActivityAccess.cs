using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Enums;
using Eagles.Application.Model.Common;
using Eagles.DomainService.Model.Activity;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.ActivityAccess
{
    public interface IActivityAccess : IInterfaceBase
    {
        int CreateActivity(DomainService.Model.Activity.TbActivity activity);

        int EditActivityJoin(int orgId, int branchId, int activityId, int userId);

        int EditActivityReview(ActivityTypeEnum type, int activityId);

        bool EditActivityComplete(int activityId);
        
        int EditActivityFeedBack(int activityyId, string content, List<Attachment> list);
        
        List<TbActivity> GetActivity(ActivityType activityType,int branchId);

       // List<TbActivity> GetActivity(ActivityType activityType, int branchId);
        List<TbUserActivity> GetUserActivity(int userId);

        TbActivity GetActivityDetail(int activityId);

        List<TbActivity> GetPublicActivity(ActivityType activityType, int appId);
    }
}