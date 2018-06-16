using System.Collections.Generic;
using Eagles.Base;
using Eagles.Application.Model.Common;

namespace Eagles.Interface.Core.DataBase.ActivityAccess
{
    public interface IActivityAccess : IInterfaceBase
    {
        int CreateActivity(DomainService.Model.Activity.Activity activity);

        int EditActivityComment(int activityId, int userId, string content);

        int EditActivityComplete(int activityId);

        int EditActivityFeedBack(int activityyId, string content, List<Attachment> list);

        int EditActivityJoin(int activityId);

        List<Eagles.DomainService.Model.Activity.Activity> GetActivity(int activityType);

        DomainService.Model.Activity.Activity GetActivityDetail(int activityId);

        List<DomainService.Model.User.UserComment> GetActivityComment(int activityId);
        
    }
}