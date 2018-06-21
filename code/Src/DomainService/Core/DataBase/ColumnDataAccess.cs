using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.Column.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.App;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class ColumnDataAccess: IColumnDataAccess
    {

        private readonly IDbManager dbManager;

        public ColumnDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbAppModule> GetColumnList(GetColumnRequset requset)
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

            if (!string.IsNullOrWhiteSpace(requset.ColumnName))
            {
                parameter.Append(" and ModuleName = @ModuleName ");
                dynamicParams.Add("ModuleName", requset.ColumnName);
            }

            //if (requset.Status > 0)
            //{
            //    parameter.Append(" and Status = @Status ");
            //    dynamicParams.Add("Status", (int)requset.Status);
            //}


            sql.AppendFormat(@" SELECT `tb_app_module`.`OrgId`,
    `tb_app_module`.`ModuleId`,
    `tb_app_module`.`ModuleName`,
    `tb_app_module`.`TragetUrl`,
    `tb_app_module`.`ModuleType`,
    `tb_app_module`.`SmallImageUrl`,
    `tb_app_module`.`ImageUrl`,
    `tb_app_module`.`Priority`,
    `tb_app_module`.`IndexPageCount`,
    `tb_app_module`.`IndexDisplay`
FROM `eagles`.`tb_app_module`   where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbAppModule>(sql.ToString(), dynamicParams);
        }

        public TbAppModule GetColumnDetail(GetColumnDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_app_module`.`OrgId`,
    `tb_app_module`.`ModuleId`,
    `tb_app_module`.`ModuleName`,
    `tb_app_module`.`TragetUrl`,
    `tb_app_module`.`ModuleType`,
    `tb_app_module`.`SmallImageUrl`,
    `tb_app_module`.`ImageUrl`,
    `tb_app_module`.`Priority`,
    `tb_app_module`.`IndexPageCount`,
    `tb_app_module`.`IndexDisplay`
FROM `eagles`.`tb_app_module`  where ModuleId=@ColumnId;
 ");
            dynamicParams.Add("ColumnId", requset.ColumnId);

            return dbManager.QuerySingle<TbAppModule>(sql.ToString(), dynamicParams);
        }

        public int RemoveColumn(RemoveColumnRequset requset)
        {
            return dbManager.Excuted(@"  DELETE FROM `eagles`.`tb_app_module`  where
                `ModuleId` = @ColumnId;
", new { requset.ColumnId });
        }

        public int EditColumn(TbAppModule mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_app_module`
SET
`OrgId` = @OrgId,
`ModuleName` = @ModuleName,
`TragetUrl` = @TragetUrl,
`ModuleType` = @ModuleType,
`SmallImageUrl` = @SmallImageUrl,
`ImageUrl` = @ImageUrl,
`Priority` = @Priority,
`IndexPageCount` = @IndexPageCount,
`IndexDisplay` = @IndexDisplay
WHERE `ModuleId` = @ModuleId;

", mod);

        }

        public int CreateColumn(TbAppModule mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_app_module`
(`OrgId`,
`ModuleId`,
`ModuleName`,
`TragetUrl`,
`ModuleType`,
`SmallImageUrl`,
`ImageUrl`,
`Priority`,
`IndexPageCount`,
`IndexDisplay`)
VALUES
(@OrgId ,
@ModuleId ,
@ModuleName ,
@TragetUrl ,
@ModuleType ,
@SmallImageUrl ,
@ImageUrl ,
@Priority ,
@IndexPageCount ,
@IndexDisplay );
", mod);        
        }
    }
}
