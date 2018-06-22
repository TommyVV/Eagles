using System;

namespace Eagles.Application.Model.Organization.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizationDetail : Organization
    {
      

        /// <summary>
        /// '修改时间',
        /// </summary>
        public DateTime EditTime { get; set; }

        /// <summary>
        /// '操作员'
        /// </summary>
        public int OperId { get; set; }

        /// <summary>
        /// '组织logo',
        /// </summary>
        public string Logo { get; set; }
    }
}
