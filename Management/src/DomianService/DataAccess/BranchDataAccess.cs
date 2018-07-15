using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.Branch.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Org;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class BranchDataAccess : IBranchDataAccess
    {
        private readonly IDbManager dbManager;

        public BranchDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbBranch> GetBranchList(GetBranchRequset requset, out int totalcount)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.OrgId>0)
            {
                parameter.Append(" and OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }

         
            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_branch`  where 1=1  {0} ;
 ", parameter);
            totalcount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);




            sql.AppendFormat(@" SELECT `tb_branch`.`OrgId`,
    `tb_branch`.`BranchId`,
    `tb_branch`.`BranchName`,
    `tb_branch`.`BranchDesc`,
    `tb_branch`.`CreateTime`
FROM `eagles`.`tb_branch`  where 1=1    {0}  order by BranchId desc limit  @pageStart ,@pageSize
 ", parameter);




            return dbManager.Query<TbBranch>(sql.ToString(), dynamicParams);
        }

        public TbBranch GetBranchDetail(GetBranchDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@"  SELECT `tb_branch`.`OrgId`,
    `tb_branch`.`BranchId`,
    `tb_branch`.`BranchName`,
    `tb_branch`.`BranchDesc`,
    `tb_branch`.`CreateTime`
FROM `eagles`.`tb_branch`  where BranchId=@BranchId;
 ");
            dynamicParams.Add("BranchId", requset.BranchId);

            return dbManager.QuerySingle<TbBranch>(sql.ToString(), dynamicParams);
        }

        public int RemoveBranch(RemoveBranchRequset requset)
        {
            return dbManager.Excuted(@"  DELETE FROM `eagles`.`tb_branch`  where BranchId=@BranchId;
", new { requset.BranchId });
        }

        public int EditBranch(TbBranch mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_branch`
SET
`OrgId` =@OrgId,
`BranchName` =@BranchName,
`BranchDesc` =@BranchDesc,
`CreateTime` =@CreateTime
WHERE `
`BranchId` =@BranchId

", mod);
        }

        public int CreateBranch(TbBranch mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_branch`
(`OrgId`,
`BranchId`,
`BranchName`,
`BranchDesc`,
`CreateTime`)
VALUES
(@OrgId,
@BranchId,
@BranchName,
@BranchDesc,
@CreateTime);

", mod);
        }

        //public List<TB_USER_RELATIONSHIP> GetBranchList(List<UserInfoDetails> list)
        //{
        //    throw new NotImplementedException();
        //}



    }
}
