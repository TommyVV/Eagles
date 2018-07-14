using System;

namespace Eagles.Application.Model.Organization.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Organization
    {

        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 省id
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市id
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区id
        /// </summary>
        public string District { get; set; }

        ///// <summary>
        ///// 下级机构id
        ///// </summary>
        //public int BranchOrgId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
    }
}
