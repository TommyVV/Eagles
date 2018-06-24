using System.Collections.Generic;
using Eagles.Application.Model.Common;
using Eagles.Application.Model.Enums;
using Eagles.Base;

namespace Eagles.Interface.DataAccess.ActivityAccess
{
    public interface IActivityAccess : IInterfaceBase
    {
        int CreateActivity(DomainService.Model.Activity.TbActivity activity);

        int EditActivityJoin(int orgId, int branchId, int activityId, int userId);

        int EditActivityReview(ActivityTypeEnum type, int activityId);

        bool EditActivityComplete(int activityId);
        
        int EditActivityFeedBack(int activityyId, string content, List<Attachment> list);
        
        List<DomainService.Model.Activity.TbActivity> GetActivity(int activityType);

        DomainService.Model.Activity.TbActivity GetActivityDetail(int activityId);
        
    }
}