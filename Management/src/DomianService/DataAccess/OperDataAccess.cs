using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.Login.Requset;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Oper;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
   public class OperDataAccess: IOperDataAccess
    {
        private readonly IDbManager dbManager;

        public OperDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public int EditOper(TbOper mod)
        {
            return dbManager.Excuted(@" UPDATE `eagles`.`tb_oper`
SET

`OperId` = @OperId,
`OperName` = @OperName,
`CreateTime` = @CreateTime,
`GroupId` = @GroupId,
`Status` = @Status,
`Password` = @Password
WHERE `OrgId` = @OrgId
 ", mod);
        }

        public int CreateOper(TbOper mod)
        {
            return dbManager.Excuted(@" INSERT INTO `eagles`.`tb_oper`
(`OrgId`,
`OperId`,
`OperName`,
`CreateTime`,
`GroupId`,
`Status`,
`Password`)
VALUES
(@OrgId,
@OperId,
@OperName,
@CreateTime,
@GroupId,
@Status,
@Password);
 ", mod);
        }

        public int RemoveOper(RemoveOperatorRequset requset)
        {
            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_oper` 
              where OperId=@OperId
", new { requset.OperId });
        }

        public TbOper GetOperDetail(GetOperatorDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_oper`.`OrgId`,
    `tb_oper`.`OperId`,
    `tb_oper`.`OperName`,
    `tb_oper`.`CreateTime`,
    `tb_oper`.`GroupId`,
    `tb_oper`.`Status`,
    `tb_oper`.`Password`
FROM `eagles`.`tb_oper`
  where OperId=@OperId;
 ");
            dynamicParams.Add("OperId", requset.OperId);

            return dbManager.QuerySingle<TbOper>(sql.ToString(), dynamicParams);
        }

        public List<TbOper> GetOperList(GetOperatorRequset requset)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (requset.OrgId > 0)
            {
                parameter.Append(" and  OrgId = @OrgId ");
                dynamicParams.Add("OrgId", requset.OrgId);
            }

         

            if (requset.StartTime != null)
            {
                parameter.Append(" and CreateTime <= @StartTime ");
                dynamicParams.Add("StartTime", requset.StartTime);
            }

            if (requset.EndTime != null)
            {
                parameter.Append(" and CreateTime >= @EndTime ");
                dynamicParams.Add("EndTime", requset.EndTime);
            }

            sql.AppendFormat(@" SELECT `tb_oper`.`OrgId`,
    `tb_oper`.`OperId`,
    `tb_oper`.`OperName`,
    `tb_oper`.`CreateTime`,
    `tb_oper`.`GroupId`,
    `tb_oper`.`Status`,
    `tb_oper`.`Password`
FROM `eagles`.`tb_oper`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbOper>(sql.ToString(), dynamicParams);
        }

        public int GetOperListByAuthorityGroupId(int requsetAuthorityGroupId)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_oper`.`OrgId`,
    `tb_oper`.`OperId`,
    `tb_oper`.`OperName`,
    `tb_oper`.`CreateTime`,
    `tb_oper`.`GroupId`,
    `tb_oper`.`Status`,
    `tb_oper`.`Password`
FROM `eagles`.`tb_oper`
  where GroupId=@GroupId;
 ");
            dynamicParams.Add("GroupId", requsetAuthorityGroupId);

            return dbManager.Query<TbOper>(sql.ToString(), dynamicParams).Count;
        }

        public TbOper GetOperInfo(LoginRequset requset)
        {
            throw new NotImplementedException();
        }
    }
}
