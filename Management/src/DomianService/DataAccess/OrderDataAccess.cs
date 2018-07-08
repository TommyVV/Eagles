using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.DeliverGoods.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Order;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class OrderDataAccess : IOrderDataAccess
    {
        private readonly IDbManager dbManager;

        public OrderDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public List<TbOrder> GetOrderList(GetDeliverGoodsRequset requset)
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

            if (!string.IsNullOrWhiteSpace(requset.GoodsName))
            {
                parameter.Append(" and ProdName = @GoodsName ");
                dynamicParams.Add("GoodsName", requset.GoodsName);
            }



            sql.AppendFormat(@" SELECT `tb_order`.`OrgId`,
    `tb_order`.`OrderId`,
    `tb_order`.`ProdId`,
    `tb_order`.`ProdName`,
    `tb_order`.`OrderStatus`,
    `tb_order`.`Score`,
    `tb_order`.`Count`,
    `tb_order`.`UserId`,
    `tb_order`.`ExpressId`,
    `tb_order`.`Address`,
    `tb_order`.`Province`,
    `tb_order`.`City`,
    `tb_order`.`District`,
    `tb_order`.`CreateTime`,
    `tb_order`.`UpdateTime`,
    `tb_order`.`OperId`
FROM `eagles`.`tb_order`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbOrder>(sql.ToString(), dynamicParams);
        }

        public TbOrder GetOrderDetail(GetDeliverGoodsDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_order`.`OrgId`,
    `tb_order`.`OrderId`,
    `tb_order`.`ProdId`,
    `tb_order`.`ProdName`,
    `tb_order`.`OrderStatus`,
    `tb_order`.`Score`,
    `tb_order`.`Count`,
    `tb_order`.`UserId`,
    `tb_order`.`ExpressId`,
    `tb_order`.`Address`,
    `tb_order`.`Province`,
    `tb_order`.`City`,
    `tb_order`.`District`,
    `tb_order`.`CreateTime`,
    `tb_order`.`UpdateTime`,
    `tb_order`.`OperId`
FROM `eagles`.`tb_order`
  where OrderId=@OrderId;
 ");
            dynamicParams.Add("OrderId", requset.OrderId);

            return dbManager.QuerySingle<TbOrder>(sql.ToString(), dynamicParams);
        }

        public int EditOrder(List<TbOrder> mod)
        {
            return dbManager.Excuted(@" UPDATE `eagles`.`tb_order`
SET
`ExpressId` =@ExpressId,
`Address` =@Address
WHERE `OrderId` =@OrderId;

 ", mod);
        }
    }
}
