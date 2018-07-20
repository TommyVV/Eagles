using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class ReqUser
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
        public string Gender { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string Ethnic { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birth { get; set; }
        
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
        /// 工作单位
        /// </summary>
        public string Employer { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        
        /// <summary>
        /// 头像Url
        /// </summary>
        public string PhotoUrl { get; set; }
    }
}