using System.Collections.Generic;

namespace Eagles.Application.Model.Operator.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class OperatorResponse:ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Operator> List { get; set; }
    }
}
