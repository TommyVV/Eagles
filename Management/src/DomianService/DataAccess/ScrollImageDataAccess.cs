﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.RollImage.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.ScrollImage;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class ScrollImageDataAccess: IScrollImageDataAccess
    {

        private readonly IDbManager dbManager;

        public ScrollImageDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }
        public int EditRollImages(TbScrollImage mod)
        {
            return dbManager.Excuted(@"UPDATE `eagles`.`tb_scroll_image`
SET
`OrgId` = @OrgId,
`PageType` = @PageType,
`ImageUrl` = @ImageUrl,
`TargetUrl` = @TargetUrl
WHERE `Id` = @Id

", mod);
        }

        public int CreateRollImages(TbScrollImage mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_scroll_image`
(`OrgId`,
`Id`,
`PageType`,
`ImageUrl`,
`TargetUrl`)
VALUES
(@OrgId,
@Id,
@PageType,
@ImageUrl,
@TargetUrl);


", mod);
        }

        public int RemoveRollImages(RemoveRollImageRequset requset)
        {
            return dbManager.Excuted(@"DELETE FROM `eagles`.`tb_scroll_image`
WHERE
                `Id` = @Id;
", new { requset.Id });
        }

        public TbScrollImage GetRollImagesDetail(int id)
        {
            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT  a.OrgId,
                a.Id,
                a.PageType,
                a.ImageUrl,
               a.TargetUrl,
                b.OrgName
            FROM eagles.tb_scroll_image  as a
            inner join eagles.tb_org_info b on a.OrgId=b.orgId
  where Id=@Id;
 ");
            dynamicParams.Add("Id", id);

            return dbManager.QuerySingle<TbScrollImage>(sql.ToString(), dynamicParams);
        }

        public List<TbScrollImage> GetRollImagesList(GetRollImageRequest requset ,out int totalCount,int orgId)
        {

            var sql = new StringBuilder();
            var parameter = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            //if (!string.IsNullOrWhiteSpace(requset))
            //{
            //    parameter.Append(" and PageType = @PageType ");
            //    dynamicParams.Add("PageType", requset.PageId);
            //}

            if (orgId > 0)
            {
                parameter.Append(" and OrgId = @OrgId ");
                dynamicParams.Add("OrgId", orgId);
            }



            sql.AppendFormat(@"SELECT count(*)
FROM `eagles`.`tb_scroll_image`  where 1=1  {0} ;
 ", parameter);
            totalCount = dbManager.ExecuteScalar<int>(sql.ToString(), dynamicParams);

            sql.Clear();

            dynamicParams.Add("pageStart", (requset.PageNumber - 1) * requset.PageSize);
            dynamicParams.Add("pageNum", requset.PageNumber);
            dynamicParams.Add("pageSize", requset.PageSize);

            sql.AppendFormat(@" SELECT  a.OrgId,
                a.Id,
                a.PageType,
                a.ImageUrl,
               a.TargetUrl,
                b.OrgName
            FROM eagles.tb_scroll_image  as a
            inner join eagles.tb_org_info b on a.OrgId=b.orgId  where 1 = 1  {0} order by Id desc limit  @pageStart ,@pageSize
 ", parameter);


          

            return dbManager.Query<TbScrollImage>(sql.ToString(), dynamicParams);
        }
    }
}
