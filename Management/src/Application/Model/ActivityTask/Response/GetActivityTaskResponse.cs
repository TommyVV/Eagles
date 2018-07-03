using System.Collections.Generic;

namespace Eagles.Application.Model.ActivityTask.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetActivityTaskResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.ActivityTaskModel> List { get; set; }
    }
}
