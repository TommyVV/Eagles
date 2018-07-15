using System.Collections.Generic;

namespace Eagles.Application.Model.Branch.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetBranchResponse
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Branch> List { get; set; }
    }
}
