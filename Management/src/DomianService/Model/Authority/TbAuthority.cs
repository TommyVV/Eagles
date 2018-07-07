using System;

namespace Eagles.DomainService.Model.Authority
{
    /// <summary>
    /// TB_AUTHORITY
    /// </summary>
    public class TbAuthority
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 功能id
        /// </summary>
        public string FunCode { get; set; }
        /// <summary>
        /// 权限组编号
        /// </summary>
        public int GroupId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
    }
}