using System;

namespace Eagles.Application.Model
{
    /// <summary>
    /// 基本的 凭证
    /// </summary>
    public class RequestBase
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }

    }



    /// <summary>
    /// 分页用的list
    /// </summary>
    public class PageRequestBase : RequestBase
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 页尺寸
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

    }


    ///// <summary>
    ///// 机构和组织
    ///// </summary>
    //public class OrgBranchRequest : RequestBase
    //{
    //    /// <summary>
    //    /// 机构id
    //    /// </summary>
    //    public int OrgId { get; set; }

    //    /// <summary>
    //    /// 支部id
    //    /// </summary>
    //    public int BranchId { get; set; }
    //}

    ///// <summary>
    ///// 机构和组织
    ///// </summary>
    //public class OrgRequest : RequestBase
    //{
    //    /// <summary>
    //    /// 机构id
    //    /// </summary>
    //    public int OrgId { get; set; }


    //}



}
