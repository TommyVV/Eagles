using System;

namespace Eagles.DomainService.Model.Org
{
    /// <summary>
    /// TB_ORG_INFO
    /// </summary>
    public class TB_ORG_INFO
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }
        /// <summary>
        /// 组织logo
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
    }
}