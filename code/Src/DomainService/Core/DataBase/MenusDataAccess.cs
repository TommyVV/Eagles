﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.App;
using Eagles.DomainService.Model.Product;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class MenusDataAccess : IMenusDataAccess
    {
        private readonly IDbManager dbManager;

        public MenusDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public int EditNews(TbAppMenu mod)
        {
            return dbManager.Excuted(@" UPDATE `eagles`.`tb_app_menu`
SET
`OrgId` = @OrgId,
`MenuName` = @MenuName,
`Level` = @Level,
`ParentMenuId` = @ParentMenuId,
`TragetUrl` = @TragetUrl
WHERE `MenuId` = @MenuId

 ", mod);
        }

        public int CreateNews(TbAppMenu mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_app_menu`
(`OrgId`,
`MenuId`,
`MenuName`,
`Level`,
`ParentMenuId`,
`TragetUrl`)
VALUES
(@OrgId,
@MenuId,
@MenuName,
@Level,
@ParentMenuId,
@TragetUrl);



", mod);
        }

        public int RemoveMenus(RemoveMenusRequset requset)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_app_menu`
WHERE
              `MenuId` = @MenuId
", new { requset.MenuId });
        }

        public TbAppMenu GetMenusDetail(GetMenusDetailRequest requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_app_menu`.`OrgId`,
    `tb_app_menu`.`MenuId`,
    `tb_app_menu`.`MenuName`,
    `tb_app_menu`.`Level`,
    `tb_app_menu`.`ParentMenuId`,
    `tb_app_menu`.`TragetUrl`
FROM `eagles`.`tb_app_menu`
  where MenuId=@MenuId;
 ");
            dynamicParams.Add("ProdId", requset.MenuId);

            return dbManager.QuerySingle<TbAppMenu>(sql.ToString(), dynamicParams);
        }

        public List<TbAppMenu> GetNewsList(GetMenusRequset requset)
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

            if (requset.MenuLevel > 0)
            {
                parameter.Append(" and Level = @MenuLevel ");
                dynamicParams.Add("Level", requset.MenuLevel);
            }


            sql.AppendFormat(@" SELECT `tb_app_menu`.`OrgId`,
    `tb_app_menu`.`MenuId`,
    `tb_app_menu`.`MenuName`,
    `tb_app_menu`.`Level`,
    `tb_app_menu`.`ParentMenuId`,
    `tb_app_menu`.`TragetUrl`
FROM `eagles`.`tb_app_menu`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbAppMenu>(sql.ToString(), dynamicParams);
        }
    }
}