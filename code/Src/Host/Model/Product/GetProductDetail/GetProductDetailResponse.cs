using System;

namespace Eagles.Application.Model.Product.GetProductDetail
{
    /// <summary>
    /// 积分商品详情查询
    /// </summary>
    public class GetProductDetailResponse
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品积分
        /// </summary>
        public int ProductScore { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProductImgUrl { get; set; }

        /// <summary>
        /// 产品上架时间
        /// </summary>
        public DateTime ProductBeginTime { get; set; }

        /// <summary>
        /// 产品下架时间
        /// </summary>
        public DateTime ProductEndTime { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string ProductDescrption { get; set; }

        /// <summary>
        /// 兑换人数
        /// </summary>
        public int PeopleCount { get; set; }

        /// <summary>
        /// 产品库存
        /// </summary>
        public int Inventory { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public int SalesCount { get; set; }

        /// <summary>
        /// 产品价值
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 限购数量
        /// </summary>
        public int LimitedCount { get; set; }
        
    }
}