using System.Collections.Generic;

namespace Eagles.Application.Model.News.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetNewResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.New> List { get; set; }
    }
}
