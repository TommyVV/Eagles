using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.ActivityTask.Requset;
using Eagles.Application.Model.ActivityTask.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IActivityTaskHandler : IInterfaceBase
    {

        /// <summary>
        /// 编辑 活动
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool EditActivity(EditActivityTaskInfoRequset requset);

        /// <summary>
        /// 删除 活动
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveActivity(RemoveActivityTaskRequset requset);

        /// <summary>
        /// 活动 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetActivityTaskDetailResponse GetActivityDetail(GetActivityTaskDetailRequset requset);

        /// <summary>
        /// 活动 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetActivityTaskResponse GetActivity(GetActivityTaskRequset requset);
    }
}
