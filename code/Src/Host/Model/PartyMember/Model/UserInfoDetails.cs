using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.PartyMember.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfoDetails : Member
    {
        /// <summary>
        /// 
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Nation { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birth { get; set; }

        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }

        /// <summary>
        /// 户籍地址
        /// </summary>
        public string FamilyAddress { get; set; }

        /// <summary>
        /// 常用地址
        /// </summary>
        public string DefaultAddress { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 学历
        /// </summary>
        public string Education { get; set; }

        /// <summary>
        /// 毕业学校
        /// </summary>
        public string GraduateSchool { get; set; }

        /// <summary>
        /// 公司
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// 职位
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 预备党员日期
        /// </summary>
        public DateTime BeforJoinTime { get; set; }

        /// <summary>
        /// 转正加入时间
        /// </summary>
        public DateTime FormalJoinTime { get; set; }

        /// <summary>
        /// 是否缴纳党费
        /// </summary>
        public bool IsMoney { get; set; }

        /// <summary>
        /// 人员类别
        /// </summary>
        public int UserStatus { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 游客;
        /// 0:是 ;
        ///1:否',
        /// </summary>
        public int IsCustomer { get; set; }
        /// <summary>
        /// 支部编号
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
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
        /// 党员状态;0:正常;1:禁用
        /// </summary>
        public int MemberStatus { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        ///  密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string PhotoUrl { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Provice { get; set; }
        /// <summary>
        /// 用户状态;0:正常;1:禁用
        /// </summary>
        public int Status { get; set; }
    }
}
