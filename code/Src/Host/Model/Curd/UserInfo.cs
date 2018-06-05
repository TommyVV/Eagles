using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    public class UserInfo
    {
        public string Name { get; set; }

        public string Gender { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Ethnic { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birth { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string Origin { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string CensusAddress { get; set; }

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
        public string PartyType { get; set; }

        /// <summary>
        /// 党费缴纳情况
        /// </summary>
        public string PartyMembershipDues { get; set; }

        /// <summary>
        /// 我的支部
        /// </summary>
        public string Branch { get; set; }

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
    }
}
