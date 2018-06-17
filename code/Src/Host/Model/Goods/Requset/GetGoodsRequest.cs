using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Goods.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodsRequest:OrgListRequestBase
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GoodsStatus GoodsStatus { get; set; }
    }
}
