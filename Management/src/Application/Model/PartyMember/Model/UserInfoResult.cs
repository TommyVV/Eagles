namespace Eagles.Application.Model.PartyMember.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfoResult:Member
    {
        /// <summary>
        /// 检查结果
        /// </summary>
        public string Result { get; set; }

        public bool IsSystemUser { get; set; }
    }
}
