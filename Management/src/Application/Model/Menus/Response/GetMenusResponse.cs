using System.Collections.Generic;

namespace Eagles.Application.Model.Menus.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMenusResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Menus> List { get; set; }
    }
}
