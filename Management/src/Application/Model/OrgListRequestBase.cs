using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model
{
    /// <summary>
    /// 分页用的list
    /// </summary>
    public class OrgListRequestBase : OrgRequestBase
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
}
