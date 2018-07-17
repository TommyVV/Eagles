using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Goods.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetGoodsRequest : OrgListRequestBase
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 产品状态;0:上架:1:下架
        /// </summary>
        public GoodsStatus GoodsStatus { get; set; }
    }
}
