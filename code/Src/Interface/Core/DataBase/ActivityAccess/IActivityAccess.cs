using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.AppModel.Enum;

namespace Eagles.Interface.Core.DataBase.ActivityAccess
{
    public interface IActivityAccess : IInterfaceBase
    {
        int CreateActivity(DomainService.Model.Activity.Activity activity);

        int EditActivityJoin(int orgId, int branchId, int activityId, int userId);

        int EditActivityReview(ActivityTypeEnum type, int activityId);

        bool EditActivityComplete(int activityId);
        
        int EditActivityFeedBack(int activityyId, string content, List<Attachment> list);

        int EditActivityComment(int orgId, int activityId, int userId, string content);

        List<Eagles.DomainService.Model.Activity.Activity> GetActivity(int activityType);

        DomainService.Model.Activity.Activity GetActivityDetail(int activityId);

        List<DomainService.Model.User.UserComment> GetActivityComment(int activityId);
        
    }
}