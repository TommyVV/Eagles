using System.Collections.Generic;
using Eagles.Application.Model.Publicity.Model;

namespace Eagles.Application.Model.Publicity.Response
{
    public class GetPublicTaskResponse
    {
        /// <summary>
        /// 任务列表
        /// </summary>
        public List<PublicTask> Tasks { get; set; }

        public  int TotalCount { get; set; }
    }
}
