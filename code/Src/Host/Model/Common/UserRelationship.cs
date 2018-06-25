using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 上下级关系表
    /// </summary>
    public class UserRelationship
    {
        string UserId { get; set; }

        string SubUserId { get; set; }
    }
}