using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.Audit.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.App;
using Eagles.DomainService.Model.Audit;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
   public  class AuditDataAccess: IAuditDataAccess
    {
        private readonly IDbManager dbManager;

        public AuditDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public int CreateAudit(TbReview mod)
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

        public List<TbReview> GetAuditList(GetAuditRequest requset)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            //if (requset.OrgId > 0)
            //{
            //    parameter.Append(" and  OrgId = @OrgId ");
            //    dynamicParams.Add("OrgId", requset.OrgId);
            //}

            //if (requset.BranchId > 0)
            //{
            //    parameter.Append(" and BranchId = @BranchId ");
            //    dynamicParams.Add("BranchId", requset.BranchId);
            //}

            //if (!string.IsNullOrWhiteSpace(requset.ColumnName))
            //{
            //    parameter.Append(" and ModuleName = @ModuleName ");
            //    dynamicParams.Add("ModuleName", requset.ColumnName);
            //}

            //if (requset.Status > 0)
            //{
            //    parameter.Append(" and Status = @Status ");
            //    dynamicParams.Add("Status", (int)requset.Status);
            //}


            sql.AppendFormat(@" SELECT `tb_review`.`ReviewId`,
    `tb_review`.`OrgId`,
    `tb_review`.`BranchId`,
    `tb_review`.`NewsId`,
    `tb_review`.`NewsType`,
    `tb_review`.`OperId`,
    `tb_review`.`Result`,
    `tb_review`.`CreateTime`,
    `tb_review`.`ReviewStatus`
FROM `eagles`.`tb_review`   where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TbReview>(sql.ToString(), dynamicParams);
        }
    }
}
