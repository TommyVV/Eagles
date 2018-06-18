using System.Linq;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.DataBase.ProductAccess;
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

        public List<DomainModel.Product.Product> GetProduct()
        {
            return dbManager.Query<DomainModel.Product.Product>("select ProdId,ProdName,Score,ImageUrl from eagles.tb_product ", new { });
        }

        public DomainModel.Product.Product GetProductDetail(int productId)
        {
            var result = dbManager.Query<DomainModel.Product.Product>(
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