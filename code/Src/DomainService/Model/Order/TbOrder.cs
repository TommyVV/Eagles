using System;

namespace Eagles.DomainService.Model.Order
{
    /// <summary>
    /// TB_ORDER
    /// </summary>
    public class TbOrder
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int Count { get; set; }
        public DateTime CreateTime { get; set; }
        public string District { get; set; }
        public string ExpressId { get; set; }
        public int OperId { get; set; }
        public int OrderId { get; set; }
        public int OrderStatus { get; set; }
        public int OrgId { get; set; }
        public int ProdId { get; set; }
        public string ProdName { get; set; }
        public string Province { get; set; }
        public int Score { get; set; }
        public DateTime UpdateTime { get; set; }
        public int UserId { get; set; }
        public string SmallImageUrl { get; set; }
    }
}