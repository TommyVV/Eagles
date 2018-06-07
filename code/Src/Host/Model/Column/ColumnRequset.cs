using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.enums;

namespace Eagles.Application.Model.Column
{
    public class ColumnRequset:RequestBase
    {

        /// <summary>
        /// 任务/活动 名字
        /// </summary>
        public string ColumnName { get; set; }


        static readonly DateTime Dt = DateTime.Now;

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime StartTime { get; set; } = Dt;

        /// <summary>
        /// 统计结束时间
        /// </summary>
        public DateTime EndTime { get; set; } = Dt;

        /// <summary>
        /// 是否查询全部 为frue时 不带统计时间筛选条件
        /// </summary>
        public bool IsSearchDateTime { get; set; } = false;

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }
    }
}
