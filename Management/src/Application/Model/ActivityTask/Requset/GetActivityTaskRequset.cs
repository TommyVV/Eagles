using System;

namespace Eagles.Application.Model.ActivityTask.Requset
{
    /// <summary>
    /// 任务 + 活动 请求列表参数
    /// </summary>
    public class GetActivityTaskRequset : OrgListRequestBase
    {
        /// <summary>
        /// 作者id
        /// </summary>
        public int UserName { get; set; }

        /// <summary>
        /// 任务/活动 名字
        /// </summary>
        public string ActivityName { get; set; }


    }
}
