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
        public string Field { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<Entity> List { get; set; }
    }

    //entity
    public class Entity
    {
        /// <summary>
        /// 数据
        /// </summary>
        public string[] Value { get; set; }
    }
}