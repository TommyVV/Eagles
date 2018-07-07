using System;

namespace Eagles.DomainService.Model.Order
{
    /// <summary>
    /// TB_ORDER
    /// </summary>
    public class TbOrder
    {
        /// <summary>
        /// 订单寄送地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 快递id
        /// </summary>
        public string ExpressId { get; set; }
        /// <summary>
        /// 操作员id
        /// </summary>
        public int OperId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public int OrderId { get; set; }
        /// <summary>
        /// 订单状态;
        /// 0:成功
        ///1:失败
        /// </summary>
        public int OrderStatus { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary>
        public int ProdId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProdName { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 支付积分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        public int UserId { get; set; }
    }
}