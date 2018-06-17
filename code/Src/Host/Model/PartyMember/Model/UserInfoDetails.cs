using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.PartyMember.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UserInfoDetails : UserInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public Sex Sex { get; set; }

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
        public UserStatus UserStatus { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Picture { get; set; }

    }
}
