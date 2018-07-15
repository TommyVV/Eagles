namespace Eagles.Application.Model.AuthorityGroupSetUp.Requset
{
    public class GetAuthorityGroupSetUpRequest : RequestBase
    {
        /// <summary>
        /// 权限组编号
        /// </summary>
        public int GroupId { get; set; }
    }
}
