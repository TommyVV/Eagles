using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Org;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class OrganizationDataAccess : IOrganizationDataAccess
    {
        private readonly IDbManager dbManager;

        public OrganizationDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbOrgInfo> GetOrganizationList(GetOrganizationRequset requset, out int totalCount)
        {
            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(requset.City))
            {
                parameter.Append(" and City = @City ");
                dynamicParams.Add("City", requset.City);
            }

            if (!string.IsNullOrWhiteSpace(requset.District))
            {
                parameter.Append(" and District = @District ");
                dynamicParams.Add("District", requset.District);
            }

            if (!string.IsNullOrWhiteSpace(requset.Province))
            {
                parameter.Append(" and Province = @Province ");
                dynamicParams.Add("Province", requset.Province);
            }


            sql.AppendFormat(@"SELECT count(1)
FROM `eagles`.`tb_news`  where 1=1  {0} ;
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);

            sql.AppendFormat(@"  SELECT `tb_org_info`.`OrgId`,
    `tb_org_info`.`OrgName`,
    `tb_org_info`.`Province`,
    `tb_org_info`.`City`,
    `tb_org_info`.`District`,
    `tb_org_info`.`Address`,
    `tb_org_info`.`CreateTime`,
    `tb_org_info`.`EditTime`,
    `tb_org_info`.`OperId`,
    `tb_org_info`.`Logo`
FROM `eagles`.`tb_org_info`  where 1=1  {0}    order by CreateTime desc limit  @pageStart ,@pageSize
 ", parameter);



            return dbManager.Query<TbOrgInfo>(sql.ToString(), dynamicParams);
        }

        public TbOrgInfo GetOrganizationDetail(GetOrganizationDetailRequset requset)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_org_info`.`OrgId`,
    `tb_org_info`.`OrgName`,
    `tb_org_info`.`Province`,
    `tb_org_info`.`City`,
    `tb_org_info`.`District`,
    `tb_org_info`.`Address`,
    `tb_org_info`.`CreateTime`,
    `tb_org_info`.`EditTime`,
    `tb_org_info`.`OperId`,
    `tb_org_info`.`Logo`
FROM `eagles`.`tb_org_info`  where OrgId=@OrgId;
 ");
            dynamicParams.Add("OrgId", requset.OrgId);

            return dbManager.QuerySingle<TbOrgInfo>(sql.ToString(), dynamicParams);
        }

        public int RemoveOrganization(RemoveOrganizationRequset requset)
        {
            return dbManager.Excuted(@"  DELETE FROM `eagles`.`tb_org_info`  where
                `OrgId` = @OrgId;
", new { requset.OrgId });
        }

        public int EditOrganization(TbOrgInfo mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_org_info`
SET
`OrgName` = @OrgName,
`Province` = @Province,
`City` = @City,
`District` = @District,
`Address` = @Address,
`EditTime` =now(),
`OperId` = @OperId,
`Logo` = @Logo
WHERE `OrgId` = @OrgId;

", mod);
        }

        public int CreateOrganization(TbOrgInfo mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_org_info`
(`OrgName`,
`Province`,
`City`,
`District`,
`Address`,
`CreateTime`,
`EditTime`,
`OperId`,
`Logo`)
VALUES
(@OrgName,
@Province,
@City,
@District,
@Address,
now(),
now(),
@OperId,
@Logo);
", mod);
        }


        public List<TbOrgInfo> GetOrganizationList(List<int> list)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_org_info`.`OrgId`,
    `tb_org_info`.`OrgName`,
    `tb_org_info`.`Province`,
    `tb_org_info`.`City`,
    `tb_org_info`.`District`,
    `tb_org_info`.`Address`,
    `tb_org_info`.`CreateTime`,
    `tb_org_info`.`EditTime`,
    `tb_org_info`.`OperId`,
    `tb_org_info`.`Logo`
FROM `eagles`.`tb_org_info`  where  OrgId  in @OrgId
 ");
            dynamicParams.Add("OrgId", list.ToArray());

            return dbManager.Query<TbOrgInfo>(sql.ToString(), dynamicParams);

        }
    }
}
