using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.Goods.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Product;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class GoodsDataAccess: IGoodsDataAccess
    {

        private readonly IDbManager dbManager;

        public GoodsDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public int EditGoods(TB_PRODUCT mod)
        {
            return dbManager.Excuted(@" UPDATE `eagles`.`tb_product`
SET
`OrgId` = @OrgId,
`ProdName` = @ProdName,
`CreateTime` = @CreateTime,
`EditTime` = @EditTime,
`Price` = @Price,
`Score` = @Score,
`Stock` = @Stock,
`SmallImageUrl` = @SmallImageUrl,
`ImageUrl` = @ImageUrl,
`MaxBuyCount` = @MaxBuyCount,
`SaleCount` = @SaleCount,
`BeginTime` = @BeginTime,
`EndTime` = @EndTime,
`HtmlDescription` = @HtmlDescription,
`Status` = @Status
WHERE 
`ProdId` = @ProdId


 ", mod);
        }

        public int CreateGoods(TB_PRODUCT mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_product`
(`ProdId`,
`OrgId`,
`ProdName`,
`CreateTime`,
`EditTime`,
`Price`,
`Score`,
`Stock`,
`SmallImageUrl`,
`ImageUrl`,
`MaxBuyCount`,
`SaleCount`,
`BeginTime`,
`EndTime`,
`HtmlDescription`,
`Status`)
VALUES
(@ProdId,
@OrgId,
@ProdName,
@CreateTime,
@EditTime,
@Price,
@Score,
@Stock,
@SmallImageUrl,
@ImageUrl,
@MaxBuyCount,
@SaleCount,
@BeginTime,
@EndTime,
@HtmlDescription,
@Status);


", mod);
        }

        public int RemoveGoods(RemoveGoodsRequset requset)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_product`
WHERE
                `ProdId` = @ProdId;
", new { ProdId= requset.GoodsId });
        }

        public TB_PRODUCT GetGoodsDetail(GetGoodsDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_product`.`ProdId`,
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
FROM `eagles`.`tb_product`
  where ProdId=@ProdId;
 ");
            dynamicParams.Add("ProdId", requset.GoodsId);

            return dbManager.QuerySingle<TB_PRODUCT>(sql.ToString(), dynamicParams);
         
        }

        public List<TB_PRODUCT> GetNewsList(GetGoodsRequest requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }

            if (requset.BranchId > 0)
            {
                parameter.Append(" and BranchId = @BranchId ");
                dynamicParams.Add("BranchId", requset.BranchId);
            }

            if (requset.GoodsStatus > 0)
            {
                parameter.Append(" and Status = @Status ");
                dynamicParams.Add("Status", requset.GoodsStatus);
            }

            if (!string.IsNullOrWhiteSpace(requset.GoodsName))
            {
                parameter.Append(" and ProdName = @ProdName ");
                dynamicParams.Add("ProdName", requset.GoodsStatus);
            }

            //if (requset.Status > 0)
            //{
            //    parameter.Append(" and Status = @Status ");
            //    dynamicParams.Add("Status", (int)requset.Status);
            //}


            sql.AppendFormat(@" SELECT `tb_product`.`ProdId`,
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
FROM `eagles`.`tb_product`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TB_PRODUCT>(sql.ToString(), dynamicParams);
        }
    }
}
