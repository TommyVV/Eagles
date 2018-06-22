using System.Collections.Generic;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.Common;
using Eagles.Base;
using Eagles.DomainService.Model.Activity;

namespace Eagles.Interface.DataAccess
{
   public interface IActivityTaskDataAccess: IInterfaceBase
    {
        int EditActivity(TbActivity mod);
        int CreateActivity(TbActivity mod);
        int RemoveActivity(RemoveActivityTaskRequset requset);
        TbActivity GetActivityDetail(GetActivityTaskDetailRequset requset);
        List<TbActivity> GetGetActivityList(GetActivityTaskRequset requset);
    }
}
