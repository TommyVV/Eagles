namespace Eagles.Application.Model.PartyMember.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Member
    {

        /// <summary>
        /// 
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 机构编号所属
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string OrgName { get; set; }


        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

      
    }
}
