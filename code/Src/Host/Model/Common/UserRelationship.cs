using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 上下级关系表
    /// </summary>
    public class UserRelationship
    {
        /// <summary>
        /// 领导id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 下级id
        /// </summary>
        public int SubUserId { get; set; }
    }
}