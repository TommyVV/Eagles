using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Publicity.Request;
using Eagles.Base;
using Eagles.DomainService.Model.Activity;

namespace Eagles.Interface.DataAccess
{
    public interface IPublicityDataAccess : IInterfaceBase
    {
        TbActivity GetPublicActivityDetail(GetPublicActivityDetailRequest requset);
        List<TbActivity> GetPublicActivity(RequestBase requset);
        TbTask GetPublicTaskDetail(GetPublicTaskDetailRequest requset);
        List<TbTask> GetPublicTask(RequestBase requset);
        List<ActivityUserCount> GetActivityUserCount();
        int GetActivityUserCount(int activityId);
    }
}
