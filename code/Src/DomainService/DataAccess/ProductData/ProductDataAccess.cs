using System.Linq;
using System.Collections.Generic;
using System.Text;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.ProductAccess;
using DomainModel = Eagles.DomainService.Model;
using Dapper;
using Eagles.Application.Model.Product.GetProduct;
using Eagles.DomainService.Model.Product;

namespace Ealges.DomianService.DataAccess.ProductData
{
    public class ProductDataAccess :IProductAccess
    {
        private readonly IDbManager dbManager;

        public ProductDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<DomainModel.Product.TbProduct> GetProduct(GetProductRequest requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

       

           
                parameter.Append(" and Status = @Status");
                dynamicParams.Add("Status", 0);


            if (!string.IsNullOrWhiteSpace(requset.ProductName))
            {
                parameter.Append(" and ProdName like @ProdName ");
                dynamicParams.Add("ProdName", "%" + requset.ProductName + "%");
            }

            sql.AppendFormat(@" select ProdId,ProdName,Score,ImageUrl from eagles.tb_product 
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<DomainModel.Product.TbProduct>(sql.ToString(), dynamicParams);
            
        }

        public DomainModel.Product.TbProduct GetProductDetail(int productId)
        {
            return dbManager.QuerySingle<TbProduct>(
                @"SELECT `tb_product`.`ProdId`,
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
                `tb_product`.`Status`
            FROM `eagles`.`tb_product` where ProdId = @ProdId ", new {ProdId = productId});
        }
    }
}