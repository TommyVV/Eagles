using System;
using System.Collections.Generic;

namespace Eagles.Application.Model.Export
{
    /// <summary>
    /// 导出
    /// </summary>
    public class ExportRequest
    {
        /// <summary>
        /// sql
        /// </summary>
        public string ExportSql { get; set; }

        /// <summary>
        /// 列名(以,隔开)
        /// </summary>
        public string ColumnName { get; set; }
    }
}