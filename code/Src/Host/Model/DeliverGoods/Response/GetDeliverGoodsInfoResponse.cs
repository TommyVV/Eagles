using Eagles.Application.Model.DeliverGoods.Model;

namespace Eagles.Application.Model.DeliverGoods.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDeliverGoodsInfoResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public Model.DeliverGoods Info { get; set; }
    }
}
