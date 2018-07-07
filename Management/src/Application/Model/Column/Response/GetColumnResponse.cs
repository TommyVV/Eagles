using System.Collections.Generic;
using Eagles.Application.Model.Column.Model;

namespace Eagles.Application.Model.Column.Response
{
     /// <summary>
     /// 
     /// </summary>
     public class GetColumnResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<ColumnInfo> List { get; set; }
    }
}
