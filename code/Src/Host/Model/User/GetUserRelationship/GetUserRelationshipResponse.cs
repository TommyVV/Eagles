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
        /// 上级
        /// </summary>
        public List<UserRelationship> SuperiorUserList { get; set; }


        /// <summary>
        /// 下级
        /// </summary>
        public List<UserRelationship> LowerUserList { get; set; }


    }
}