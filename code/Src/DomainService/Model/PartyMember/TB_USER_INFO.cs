using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.DomainService.Model.PartyMember
{
    public class TB_USER_INFO
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 支部编号
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Dept { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 学历
        /// </summary>
        public string Eduction { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public string Ethinc { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdNumber { get; set; }
        /// <summary>
        /// 游客;0:是 ;1:否'
        /// </summary>
        public int IsCustomer { get; set; }
        /// <summary>
        /// 党员状态;0:正常;1:禁用
        /// </summary>
        public int MemberStatus { get; set; }
        /// <summary>
        /// 正式党员日期
        /// </summary>
        public DateTime MemberTime { get; set; }
        /// <summary>
        /// 党员类型;0:党员;1:预备党员'
        /// </summary>
        public int MemberType { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 昵称头像
        /// </summary>
        public string NickPhotoUrl { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Origin { get; set; }
        /// <summary>
        /// 户籍地址
        /// </summary>
        public string OriginAddress { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// 预备党员日期
        /// </summary>
        public DateTime PreMemberTime { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Provice { get; set; }
        /// <summary>
        /// 毕业学校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 性别;0:男;1:女
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 用户状态;0:正常;1:禁用
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
    }
}
