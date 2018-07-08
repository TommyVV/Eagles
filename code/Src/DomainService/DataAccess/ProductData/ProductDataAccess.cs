using Dapper;
using System.Text;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Product;
using Eagles.Interface.DataAccess.ProductAccess;
using Eagles.Application.Model.Product.GetProduct;

namespace Ealges.DomianService.DataAccess.ProductData
{
    public class ProductDataAccess :IProductAccess
    {
        private readonly IDbManager dbManager;

        public ProductDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        
        public List<TbProduct> GetProduct(GetProductRequest requset)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            parameter.Append(" and Status = @Status ");
            dynamicParams.Add("Status", 0);
            if (!string.IsNullOrWhiteSpace(requset.ProductName))
            {
                parameter.Append(" and ProdName like @ProdName ");
                dynamicParams.Add("ProdName", "%" + requset.ProductName + "%");
            }
            int pageIndexParameter = (requset.PageIndex - 1) * requset.PageSize;
            dynamicParams.Add("PageIndex", pageIndexParameter);
            dynamicParams.Add("PageSize", requset.PageSize);
            sql.AppendFormat(@"select ProdId,ProdName,Score,ImageUrl from eagles.tb_product where 1=1 {0} limit @PageIndex, @PageSize ", parameter);
            return dbManager.Query<TbProduct>(sql.ToString(), dynamicParams);
        }

        public TbProduct GetProductDetail(int productId)
        {
            return dbManager.QuerySingle<TbProduct>(@"SELECT `tb_product`.`ProdId`,
`tb_product`.`OrgId`,
`tb_product`.`ProdName`,
`tb_product`.`CreateTime`,
`tb_product`.`EditTime`,
`tb_product`.`Price`,
`tb_product`.`Score`,
`tb_product`.`Stock`,
`tb_product`.`SmallImageUrl`,
`tb_product`.`ImageUrl`,
`tb_product`.`MaxBuyCount`,
`tb_product`.`SaleCount`,
`tb_product`.`BeginTime`,
`tb_product`.`EndTime`,
`tb_product`.`HtmlDescription`,
`tb_product`.`Status` FROM `eagles`.`tb_product` where ProdId = @ProdId ", new {ProdId = productId});
        }
        
        public int GetOrderByProduct(int productId, int userId)
        {
            return dbManager.ExecuteScalar<int>("select count(*) from eagles.tb_order where ProdId = @ProdId and UserId = @UserId;", new { ProdId = productId, UserId = userId });
        }

    }
}