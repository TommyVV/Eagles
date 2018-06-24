using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.ProductAccess;
using DomainModel = Eagles.DomainService.Model;

namespace Ealges.DomianService.DataAccess.ProductData
{
    public class ProductDataAccess :IProductAccess
    {
        private readonly IDbManager dbManager;

        public ProductDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<DomainModel.Product.TbProduct> GetProduct()
        {
            return dbManager.Query<DomainModel.Product.TbProduct>("select ProdId,ProdName,Score,ImageUrl from eagles.tb_product ", new { });
        }

        public DomainModel.Product.TbProduct GetProductDetail(int productId)
        {
            var result = dbManager.Query<DomainModel.Product.TbProduct>(
                "select ProdId,ProdName,Score,ImageUrl,SaleCount,BeginTime,EndTime,HtmlDescription from eagles.tb_product where ProdId = @ProdId ",
                new {ProdId = productId});
            if (result != null && result.Any())
            {
                return result.FirstOrDefault();
            }
            return null;
        }
    }
}