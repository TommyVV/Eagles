using Eagles.Application.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Column
{
    public class ColumnInfo
    {

        /// <summary>
        /// 栏目id
        /// </summary>
        public int ColumnId { get; set; }

        /// <summary>
        /// 栏目名字
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 栏目地址
        /// </summary>
        public string ColumnAddress { get; set; }


        /// <summary>
        /// 审核状态
        /// </summary>
        public AuditStatus AuditStatus { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int OrderBy { get; set; }

    }
}
