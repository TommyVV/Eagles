using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.Column.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.App;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class ColumnDataAccess : IColumnDataAccess
    {

        private readonly IDbManager dbManager;

        public ColumnDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbAppModule> GetColumnList(GetColumnRequset requset, out int totalCount,int orgId)
        {


            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (orgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", orgId);
            }

            //if (requset.BranchId > 0)
            //{
            //    parameter.Append(" and BranchId = @BranchId ");
            //    dynamicParams.Add("BranchId", requset.BranchId);
            //}

            

            //if (requset.Status > 0)
            //{
            //    parameter.Append(" and Status = @Status ");
            //    dynamicParams.Add("Status", (int)requset.Status);
            //}


            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_app_module`  where 1=1  {0} ;
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);




            sql.AppendFormat(@" SELECT `tb_app_module`.`OrgId`,
    `tb_app_module`.`ModuleId`,
    `tb_app_module`.`ModuleName`,
    `tb_app_module`.`TargetUrl`,
    `tb_app_module`.`ModuleType`,
    `tb_app_module`.`SmallImageUrl`,
    `tb_app_module`.`ImageUrl`,
    `tb_app_module`.`Priority`,
    `tb_app_module`.`IndexPageCount`,
    `tb_app_module`.`IndexDisplay`
FROM `eagles`.`tb_app_module`   where 1=1  {0}  order by ModuleId desc limit  @pageStart ,@pageSize
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
    `tb_app_module`.`TargetUrl`,
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
`TargetUrl` = @TargetUrl,
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
`TargetUrl`,
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
@TargetUrl ,
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
