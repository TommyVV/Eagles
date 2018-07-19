using System.Collections.Generic;

namespace Eagles.Application.Model.Export
{
    /// <summary>
    /// 导出
    /// </summary>
    public class ExportRequest
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnNames { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<Row> RowsList { get; set; }
    }

    //entity
    public class Row
    {
        /// <summary>
        /// 数据
        /// </summary>
        public string[] Rows { get; set; }
    }
}