using System.Collections.Generic;
using Eagles.Application.Model.SystemMessage.Model;

namespace Eagles.Application.Model.SystemMessage.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSystemNewsResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<SystemNews> List { get; set; }
    }
}
