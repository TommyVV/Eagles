using System;

namespace Eagles.Application.Model.Goods.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodsDetail : Goods
    {

        /// <summary>
        /// 已售
        /// </summary>
        public int Sale { get; set; }

        /// <summary>
        /// 销售开始时间
        /// </summary>
        public DateTime SellStartTime { get; set; }

        /// <summary>
        /// 销售结束时间
        /// </summary>
        public DateTime SellEndTime { get; set; }

        /// <summary>
        /// 每人最大兑换数量
        /// </summary>
        public int MaxExchangeNum { get; set; }

        /// <summary>
        /// 参考价格
        /// </summary>
        public decimal ReferePrice { get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 小图
        /// </summary>
        public string GoodsIcon { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        public string GoodsImg { get; set; }

    }
}
