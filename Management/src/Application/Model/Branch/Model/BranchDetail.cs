using System;

namespace Eagles.Application.Model.Branch.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class BranchDetail : Branch
    {

        /// <summary>
        /// 创建时间（仅供展示，新增修改不用传递）
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 支部描述
        /// </summary>
        public string BranchDesc { get; set; }
    }
}
