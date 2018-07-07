using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.User.GetUserRelationship
{
    /// <summary>
    /// 用户上下级关系查询
    /// </summary>
    public class GetUserRelationshipResponse 
    {
        /// <summary>
        /// 用户列表
        /// </summary>
        public List<UserRelationship> UserList { get; set; }




    }
}