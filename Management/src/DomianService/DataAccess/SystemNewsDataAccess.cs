﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.SystemMessage.Requset;
using Eagles.Base.Cache;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class SystemNewsDataAccess : ISystemNewsDataAccess
    {
        private readonly IDbManager dbManager;

        private readonly ICacheHelper cacheHelper;
        public SystemNewsDataAccess(IDbManager dbManager, ICacheHelper cacheHelper)
        {
            this.dbManager = dbManager;
            this.cacheHelper = cacheHelper;
        }
        public int EditSystemNews(TbSystemNews mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_system_news`
SET

`NewsName` = @NewsName,
`NewsContent` = @NewsContent,
`NoticeTime` = @NoticeTime,
`Status` = @Status,
`OperId` = @OperId,
`RepeatTime` = @RepeatTime,
`HtmlDesc` = @HtmlDesc,
`NewsType` = @NewsType
WHERE `NewsId` = @NewsId



", mod);
        }

        public int CreateSystemNews(TbSystemNews mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_system_news`
(`NewsId`,
`NewsName`,
`NewsContent`,
`NoticeTime`,
`Status`,
`OperId`,
`RepeatTime`,
`HtmlDesc`,
`NewsType`)
VALUES
(@NewsId,
@NewsName,
@NewsContent,
@NoticeTime,
@Status,
@OperId,
@RepeatTime,
@HtmlDesc,
@NewsType);


", mod);
        }

        public int RemoveSystemNews(RemoveSystemNewsRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_system_news`
WHERE `NewsId` = @NewsId
", new { NewsId = requset.NewsId });
        }

        public List<TbSystemNews> SystemNews(GetSystemNewsRequset requset,out int totalCount)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();
            //var token = cacheHelper.GetData<TbUserToken>(requset.Token);

            //parameter.Append(" and OrgId = @OrgId ");
            //dynamicParams.Add("OrgId", token.OrgId);


            //if (requset.BranchId > 0)
            //{
            //    parameter.Append(" and Status = @Status ");
            //    dynamicParams.Add("Status", requset.Status);
            //}

            if (!string.IsNullOrWhiteSpace(requset.SystemMessageName))
            {
                parameter.Append(" and NewsName = @NewsName ");
                dynamicParams.Add("NewsName", requset.SystemMessageName);
            }



            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_system_news`  where 1=1  {0} ;
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);

            sql.AppendFormat(@" 
SELECT `tb_system_news`.`NewsId`,
    `tb_system_news`.`NewsName`,
    `tb_system_news`.`NewsContent`,
    `tb_system_news`.`NoticeTime`,
    `tb_system_news`.`Status`,
    `tb_system_news`.`OperId`,
    `tb_system_news`.`RepeatTime`,
    `tb_system_news`.`HtmlDesc`,
    `tb_system_news`.`NewsType`
FROM `eagles`.`tb_system_news`  where 1=1  {0}   order by NewsId desc limit  @pageStart ,@pageSize
 ", parameter);

         

            return dbManager.Query<TbSystemNews>(sql.ToString(), dynamicParams);
        }

        public TbSystemNews GetSystemNewsDetail(GetSystemNewsDetailRequset requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_system_news`.`NewsId`,
    `tb_system_news`.`NewsName`,
    `tb_system_news`.`NewsContent`,
    `tb_system_news`.`NoticeTime`,
    `tb_system_news`.`Status`,
    `tb_system_news`.`OperId`,
    `tb_system_news`.`RepeatTime`,
    `tb_system_news`.`HtmlDesc`,
    `tb_system_news`.`NewsType`
FROM `eagles`.`tb_system_news` 
  where NewsId=@NewsId;
 ");
            dynamicParams.Add("NewsId", requset.NewsId);

            return dbManager.QuerySingle<TbSystemNews>(sql.ToString(), dynamicParams);
        }
    }
}
