using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.Product
{
    public class TB_PRODUCT
    {
        /// <summary>
        /// 产品id
        /// </summary>
        public int ProdId { get; set; }

        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProdName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime EditTime { get; set; }

        /// <summary>
        /// 产品价值
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 购买积分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// 小图
        /// </summary>
        public string SmallImageUrl { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 每人最大购买限制
        /// </summary>
        public int MaxBuyCount { get; set; }

        /// <summary>
        /// 销售数量
        /// </summary>
        public int SaleCount { get; set; }

        /// <summary>
        /// 产品上架时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 产品下家时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 产品描述
        /// </summary>
        public string HtmlDescription { get; set; }

        /// <summary>
        /// 产品状态;0:正常:1:禁用
        /// </summary>
        public GoodsStatus Status { get; set; }
    }
}
