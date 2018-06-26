using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.User.GetUserRelationship
{
    /// <summary>
    /// 用户上下级关系查询
    /// </summary>
    public class GetUserRelationshipResponse : ResponseBase
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public List<UserRelationship> UserRelationshipList { get; set; }
    }
}