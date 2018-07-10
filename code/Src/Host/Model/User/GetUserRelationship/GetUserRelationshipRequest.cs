namespace Eagles.Application.Model.User.GetUserRelationship
{
    /// <summary>
    /// 用户上下级关系查询
    /// </summary>
    public class GetUserRelationshipRequest : RequestBase
    {
        /// <summary>
        /// 0-全部 1-上级 2-下级
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
    }
}