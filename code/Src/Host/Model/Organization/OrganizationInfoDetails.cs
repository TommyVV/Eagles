﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Organization
{
    public class OrganizationInfoDetails: OrganizationInfo
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

        /// <summary>
        /// 下级机构id
        /// </summary>
        public int BranchOrgId { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string  Address { get; set; }
    }
}