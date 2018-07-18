using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Goods.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// 商品id（新增时无需传入）
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 兑换积分积分（>=0）
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 产品状态：0:上架:1:下架
        /// </summary>
        public GoodsStatus GoodsStatus { get; set; }


    }
}
