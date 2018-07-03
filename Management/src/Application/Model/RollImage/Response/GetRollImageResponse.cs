using System.Collections.Generic;
using Eagles.Application.Model.RollImage.Model;

namespace Eagles.Application.Model.RollImage.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetRollImageResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<RollImageInfo> List { get; set; }
    }
}
