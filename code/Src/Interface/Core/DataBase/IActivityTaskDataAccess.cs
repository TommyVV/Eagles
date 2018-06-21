using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Activity;

namespace Eagles.Interface.Core.DataBase
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
