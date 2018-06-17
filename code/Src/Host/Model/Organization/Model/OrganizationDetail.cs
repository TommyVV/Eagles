namespace Eagles.Application.Model.Organization.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class OrganizationDetail : Organization
    {
        /// <summary>
        /// 省id
        /// </summary>
        public string ProvinceId { get; set; }
        /// <summary>
        /// 市id
        /// </summary>
        public string CityId { get; set; }
        /// <summary>
        /// 区id
        /// </summary>
        public string DistrictId { get; set; }

        ///// <summary>
        ///// 下级机构id
        ///// </summary>
        //public int BranchOrgId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string  Address { get; set; }
    }
}
