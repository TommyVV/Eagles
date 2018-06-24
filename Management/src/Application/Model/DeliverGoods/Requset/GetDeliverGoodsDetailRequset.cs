namespace Eagles.Application.Model.DeliverGoods.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDeliverGoodsDetailRequset : RequestBase
    {
        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderId { get; set; }
    }
}
