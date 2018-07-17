using System;

namespace Eagles.Application.Model
{
    /// <summary>
    /// 分页用的list
    /// </summary>
    public class OrgListRequestBase : RequestBase
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
        /// 统计时间（可选参数）
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 统计结束时间（可选参数）
        /// </summary>
        public DateTime? EndTime { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    public class ListRequestBase : RequestBase
    {
        /// <summary>
        /// 页码（可选参数）
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// 页尺寸（可选参数）
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
