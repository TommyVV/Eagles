using Eagles.Application.Model.Goods.Model;

namespace Eagles.Application.Model.Goods.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditGoodsRequset: RequestBase
    {
        /// <summary>
        /// 商品信息
        /// </summary>
        public GoodsDetail Info { get; set; }

        

    }
}
