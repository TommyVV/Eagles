using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int Gender { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Ethnic { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birth { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Provice { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string OriginAddress { get; set; }

        /// <summary>
        /// 常住地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Telphone { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 毕业院校
        /// </summary>
        public string School { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string Employer { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// 转预备党员日期
        /// </summary>
        public string PrepPartyDate { get; set; }

        /// <summary>
        /// 转正式党员日期
        /// </summary>
        public string FormalPartyDat { get; set; }

        /// <summary>
        /// 人员类别(正式/预备党员)
        /// </summary>
        public int PartyType { get; set; }

        /// <summary>
        /// 党费缴纳情况
        /// </summary>
        public string PartyMembershipDues { get; set; }

        /// <summary>
        /// 我的支部
        /// </summary>
        public int Branch { get; set; }

        /// <summary>
        /// 入支部日期
        /// </summary>
        public string JoinBranchDate { get; set; }

        /// <summary>
        /// 我的组织
        /// </summary>
        public string MyOrganization { get; set; }

        /// <summary>
        /// 入组织日期
        /// </summary>
        public string JoinOrganizationDate { get; set; }

        /// <summary>
        /// 党籍状态(正常/除党籍)
        /// </summary>
        public string MembershipStatus { get; set; }

        /// <summary>
        /// 头像Url
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// 是否上级
        /// </summary>
        public int IsLeader { get; set; }

        /// <summary>
        /// 用户积分
        /// </summary>
        public int Score { get; set; }
    }
}
