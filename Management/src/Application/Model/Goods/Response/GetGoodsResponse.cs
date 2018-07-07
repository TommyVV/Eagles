using System.Collections.Generic;

namespace Eagles.Application.Model.Goods.Response
{
     /// <summary>
     /// 
     /// </summary>
     public class GetGoodsResponse 
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Goods> List { get; set; }
    }
}
