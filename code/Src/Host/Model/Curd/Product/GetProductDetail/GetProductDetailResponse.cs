using System;

namespace Eagles.Application.Model.Curd.Product.GetProductDetail
{
    /// <summary>
    /// 积分商品详情查询
    /// </summary>
    public class GetProductDetailResponse : ResponseBase
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        public string ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品积分
        /// </summary>
        public string ProductScore { get; set; }

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
        public string PeopleCount { get; set; }
        
    }
}