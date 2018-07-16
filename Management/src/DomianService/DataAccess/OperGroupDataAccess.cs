using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.AuthorityGroup.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Authority;
using Eagles.DomainService.Model.Oper;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class OperGroupDataAccess : IOperGroupDataAccess
    {
        private readonly IDbManager dbManager;

        public OperGroupDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public int EditOperGroup(TbOperGroup mod)
        {
            return dbManager.Excuted(@" UPDATE `eagles`.`tb_oper_group`
SET
`OrgId` = @OrgId,
`GroupId` = @GroupId,
`GroupName` = @GroupName,
`CreateTime` = @CreateTime,
`EditTime` = @EditTime
WHERE 
`GroupId` = @GroupId

 ", mod);
        }

        public int CreateOperGroup(TbOperGroup mod)
        {
            return dbManager.Excuted(@" INSERT INTO `eagles`.`tb_oper_group`
(`OrgId`,
`GroupId`,
`GroupName`,
`CreateTime`,
`EditTime`)
VALUES
(@OrgId,
@GroupId,
@GroupName,
@CreateTime,
@EditTime);

 ", mod);
        }

        public int RemoveOperGroup(RemoveAuthorityGroupInfoRequset requset)
        {
            return dbManager.Excuted(@"  
 DELETE FROM `eagles`.`tb_oper_group`
WHERE GroupId=@GroupId
", new { GroupId = requset.AuthorityGroupId });
        }

        public TbOperGroup GetOperGroupDetail(GetAuthorityGroupDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_oper_group`.`OrgId`,
    `tb_oper_group`.`GroupId`,
    `tb_oper_group`.`GroupName`,
    `tb_oper_group`.`CreateTime`,
    `tb_oper_group`.`EditTime`
FROM `eagles`.`tb_oper_group`
  where GroupId=@GroupId;
 ");
            dynamicParams.Add("GroupId", requset.AuthorityGroupId);

            return dbManager.QuerySingle<TbOperGroup>(sql.ToString(), dynamicParams);
        }

        public List<TbOperGroup> GetOperGroupList(GetAuthorityGroupRequset requset)
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
                parameter.Append(" and CreateTime >= @StartTime ");
                dynamicParams.Add("StartTime", requset.StartTime);
            }

            if (requset.EndTime != null)
            {
                parameter.Append(" and CreateTime <= @EndTime ");
                dynamicParams.Add("EndTime", requset.EndTime);
            }

            if (!string.IsNullOrWhiteSpace(requset.AuthorityName))
            {
                parameter.Append(" and GroupName = @GroupName ");
                dynamicParams.Add("GroupName", requset.AuthorityName);
            }


            sql.AppendFormat(@" SELECT `tb_oper_group`.`OrgId`,
    `tb_oper_group`.`GroupId`,
    `tb_oper_group`.`GroupName`,
    `tb_oper_group`.`CreateTime`,
    `tb_oper_group`.`EditTime`
FROM `eagles`.`tb_oper_group`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbOperGroup>(sql.ToString(), dynamicParams);
        }

        public List<TbAuthority> GetAuthorityGroupSetUp(int groupId )
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            parameter.Append(" and  GroupId = @GroupId ");
            dynamicParams.Add("GroupId", groupId);
            sql.AppendFormat(@" SELECT `tb_authority`.`OrgId`,
    `tb_authority`.`GroupId`,
    `tb_authority`.`FunCode`,
    `tb_authority`.`OperId`,
    `tb_authority`.`CreateTime`,
    `tb_authority`.`EditTime`
FROM `eagles`.`tb_authority`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbAuthority>(sql.ToString(), dynamicParams);
        }

        public int RemoveAuthorityGroupSetUp(int groupId)
        {
            return dbManager.Excuted(@"  
 DELETE FROM `eagles`.`tb_authority`
WHERE GroupId=@GroupId
", new { GroupId = groupId });
        }

        public int CreateAuthorityGroupSetUp(List<TbAuthority> requset)
        {
            return dbManager.Excuted(@" INSERT INTO `eagles`.`tb_authority`
(`OrgId`,
`GroupId`,
`FunCode`,
`OperId`,
`CreateTime`,
`EditTime`)
VALUES
(@OrgId,
@GroupId,
@FunCode,
@OperId,
@CreateTime,
@EditTime);
 ", requset);
        }
    }
}
