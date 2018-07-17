namespace Eagles.Application.Model.PartyMember.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class RemoveUserInfoDetailsRequest:RequestBase
    {
        /// <summary>
        /// 用户id，后端生成
        /// </summary>
        public int UserId { get; set; }

    }
}
