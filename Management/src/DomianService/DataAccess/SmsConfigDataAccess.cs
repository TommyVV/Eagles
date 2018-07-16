using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.SMS.Request;
using Eagles.Base.Cache;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.Config;
using Eagles.DomainService.Model.Score;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class SmsConfigDataAccess : ISmsConfigDataAccess
    {
        private readonly IDbManager dbManager;
        private readonly ICacheHelper cacheHelper;
        public SmsConfigDataAccess(IDbManager dbManager, ICacheHelper cacheHelper)
        {
            this.dbManager = dbManager;
            this.cacheHelper = cacheHelper;
        }
        public int EditSMS(SmsConfig mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_sms_config`
SET

`VendorName` = @VendorName,
`SendCount` = @SendCount,
`CreateTime` = @CreateTime,
`AppId` = @AppId,
`AppKey` = @AppKey,
`SginKey` = @SginKey,
`ServiceUrl` = @ServiceUrl,
`MaxCount` = @MaxCount,
`Priority` = @Priority,
`Status` = @Status
WHERE `VendorId` = @VendorId


", mod);
        }

        public int CreateSMS(SmsConfig mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_sms_config`
(`VendorId`,
`VendorName`,
`SendCount`,
`CreateTime`,
`AppId`,
`AppKey`,
`SginKey`,
`ServiceUrl`,
`MaxCount`,
`Priority`,
`Status`)
VALUES
(@VendorId,
@VendorName,
@SendCount,
@CreateTime,
@AppId,
@AppKey,
@SginKey,
@ServiceUrl,
@MaxCount,
@Priority,
@Status);


", mod);
        }

        public int RemoveSMS(RemoveSMSRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_sms_config`
WHERE `VendorId` = @VendorId
", new { VendorId = requset.VendorId });
        }

        public List<SmsConfig> GetSMS(GetSMSRequset requset, out int totalCount)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            var token = cacheHelper.GetData<TbUserToken>(requset.Token);

            parameter.Append(" and OrgId = @OrgId ");
            dynamicParams.Add("OrgId", token.OrgId);

            //if (requset.BranchId > 0)
            //{
            //    parameter.Append(" and BranchId = @BranchId ");
            //    dynamicParams.Add("BranchId", requset.BranchId);
            //}

            //if (requset.OrgId > 0)
            //{
            //    parameter.Append(" and OrgId = @OrgId ");
            //    dynamicParams.Add("OrgId", requset.OrgId);
            //}



            totalCount = 0;
            sql.AppendFormat(@" SELECT `tb_sms_config`.`VendorId`,
    `tb_sms_config`.`VendorName`,
    `tb_sms_config`.`SendCount`,
    `tb_sms_config`.`CreateTime`,
    `tb_sms_config`.`AppId`,
    `tb_sms_config`.`AppKey`,
    `tb_sms_config`.`SginKey`,
    `tb_sms_config`.`ServiceUrl`,
    `tb_sms_config`.`MaxCount`,
    `tb_sms_config`.`Priority`,
    `tb_sms_config`.`Status`
FROM `eagles`.`tb_sms_config`  where 1=1  {0}  
 ", parameter);

            return dbManager.Query<SmsConfig>(sql.ToString(), dynamicParams);
        }

        public SmsConfig GetSMSDetail(GetSMSDetailRequset requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_sms_config`.`VendorId`,
    `tb_sms_config`.`VendorName`,
    `tb_sms_config`.`SendCount`,
    `tb_sms_config`.`CreateTime`,
    `tb_sms_config`.`AppId`,
    `tb_sms_config`.`AppKey`,
    `tb_sms_config`.`SginKey`,
    `tb_sms_config`.`ServiceUrl`,
    `tb_sms_config`.`MaxCount`,
    `tb_sms_config`.`Priority`,
    `tb_sms_config`.`Status`
FROM `eagles`.`tb_sms_config`
  where VendorId=@VendorId;
 ");
            dynamicParams.Add("Id", requset.VendorId);

            return dbManager.QuerySingle<SmsConfig>(sql.ToString(), dynamicParams);
        }
    }
}
