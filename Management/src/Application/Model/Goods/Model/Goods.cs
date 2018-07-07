using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Goods.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// 
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
        /// 积分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public GoodsStatus GoodsStatus { get; set; }


    }
}
