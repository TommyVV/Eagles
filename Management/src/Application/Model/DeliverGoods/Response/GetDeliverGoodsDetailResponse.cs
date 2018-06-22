using System.Collections.Generic;
using Eagles.Application.Model.DeliverGoods.Model;

namespace Eagles.Application.Model.DeliverGoods.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDeliverGoodsDetailResponse : ResponseBase
    {

        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.DeliverGoods> List { get; set; }
    }
}
