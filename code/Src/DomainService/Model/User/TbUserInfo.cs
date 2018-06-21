using System;

namespace Eagles.DomainService.Model.User
{
    /// <summary>
    /// TB_USER_INFO
    /// </summary>
    public class TbUserInfo
    {
        public string Address { get; set; }
        public DateTime Birthday { get; set; }
        public int BranchId { get; set; }
        public string City { get; set; }
        public string Company { get; set; }
        public DateTime CreateTime { get; set; }
        public string Dept { get; set; }
        public string District { get; set; }
        public DateTime EditTime { get; set; }
        public string Education { get; set; }
        public string Ethnic { get; set; }
        public string IdNumber { get; set; }
        public int IsCustomer { get; set; }
        public int MemberStatus { get; set; }
        public DateTime MemberTime { get; set; }
        public int MemberType { get; set; }
        public string Name { get; set; }
        public string NickPhotoUrl { get; set; }
        public int OperId { get; set; }
        public int OrgId { get; set; }
        public string Origin { get; set; }
        public string OriginAddress { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime PreMemberTime { get; set; }
        public string Provice { get; set; }
        public string School { get; set; }
        public int Sex { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public int IsLeader { get; set; }
        public int Score { get; set; }
    }
}