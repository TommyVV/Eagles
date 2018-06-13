using System;

namespace Eagles.DomainService.Model.Product
{
    /// <summary>
    /// TB_PRODUCT
    /// </summary>
    public class Product
    {
        public int ProdId { get; set; }
        public int OrgId { get; set; }
        public string ProdName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EditTime { get; set; }
        public decimal Price { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ImageUrl { get; set; }
        public int MaxBuyCount { get; set; }
        public int SaleCount { get; set; }
        public int Score { get; set; }
        public string SmallImageUrl { get; set; }
        public int Status { get; set; }
        public int Stock { get; set; }
        public string HtmlDescription { get; set; }
    }
}