using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Eagles.Application.Model.News.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class NewsDataAccess : INewsDataAccess
    {
        private readonly IDbManager dbManager;
        public List<TB_NEWS> GetNewsList(GetNewsRequset requset)
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

            if (!string.IsNullOrWhiteSpace(requset.NewsName))
            {
                parameter.Append(" and Title = @Title ");
                dynamicParams.Add("Title", requset.NewsName);
            }

            //if (requset.Status > 0)
            //{
            //    parameter.Append(" and Status = @Status ");
            //    dynamicParams.Add("Status", (int)requset.Status);
            //}


            sql.AppendFormat(@" SELECT `tb_news`.`OrgId`,
    `tb_news`.`NewsId`,
    `tb_news`.`ShortDesc`,
    `tb_news`.`Title`,
    `tb_news`.`HtmlContent`,
    `tb_news`.`Author`,
    `tb_news`.`Source`,
    `tb_news`.`Module`,
    `tb_news`.`Status`,
    `tb_news`.`BeginTime`,
    `tb_news`.`EndTime`,
    `tb_news`.`TestId`,
    `tb_news`.`Attach1`,
    `tb_news`.`Attach2`,
    `tb_news`.`Attach3`,
    `tb_news`.`Attach4`,
    `tb_news`.`Attach5`,
    `tb_news`.`OperId`,
    `tb_news`.`CreateTime`,
    `tb_news`.`IsImage`,
    `tb_news`.`IsVideo`,
    `tb_news`.`IsAttach`,
    `tb_news`.`IsClass`,
    `tb_news`.`IsLearning`,
    `tb_news`.`IsText`,
    `tb_news`.`ViewCount`,
    `tb_news`.`ReviewId`,
    `tb_news`.`CanStudy`,
    `tb_news`.`ImageUrl`
FROM `eagles`.`tb_news`;
  where where 1=1  {0}  
 ", parameter);

            return dbManager.Query<TB_NEWS>(sql.ToString(), dynamicParams);
        }

        public TB_NEWS GetNewsDetail(GetNewsDetailRequset requset)
        {

            var sql = new StringBuilder();
            var dynamicParams = new DynamicParameters();

            sql.Append(@" SELECT `tb_news`.`OrgId`,
    `tb_news`.`NewsId`,
    `tb_news`.`ShortDesc`,
    `tb_news`.`Title`,
    `tb_news`.`HtmlContent`,
    `tb_news`.`Author`,
    `tb_news`.`Source`,
    `tb_news`.`Module`,
    `tb_news`.`Status`,
    `tb_news`.`BeginTime`,
    `tb_news`.`EndTime`,
    `tb_news`.`TestId`,
    `tb_news`.`Attach1`,
    `tb_news`.`Attach2`,
    `tb_news`.`Attach3`,
    `tb_news`.`Attach4`,
    `tb_news`.`Attach5`,
    `tb_news`.`OperId`,
    `tb_news`.`CreateTime`,
    `tb_news`.`IsImage`,
    `tb_news`.`IsVideo`,
    `tb_news`.`IsAttach`,
    `tb_news`.`IsClass`,
    `tb_news`.`IsLearning`,
    `tb_news`.`IsText`,
    `tb_news`.`ViewCount`,
    `tb_news`.`ReviewId`,
    `tb_news`.`CanStudy`,
    `tb_news`.`ImageUrl`
FROM `eagles`.`tb_news`;
  where NewsId=@NewsId;
 ");
            dynamicParams.Add("NewsId", new { requset.NewsId });

            return dbManager.Query<TB_NEWS>(sql.ToString(), dynamicParams).FirstOrDefault();

        }

        public int RemoveNews(RemoveNewsRequset requset)
        {

            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_news`
                `NewsId` = @NewsId;
", requset.NewsId);
        }

        public int EditNews(TB_NEWS mod)
        {
            return dbManager.Excuted(@" UPDATE `eagles`.`tb_news`
SET
`OrgId` = @OrgId,
`ShortDesc` = @ShortDesc,
`Title` = @Title,
`HtmlContent` = @HtmlContent,
`Author` = @Author,
`Source` = @Source,
`Module` = @Module,
`Status` = @Status,
`BeginTime` = @BeginTime,
`EndTime` = @EndTime,
`TestId` = @TestId,
`Attach1` = @Attach1,
`Attach2` = @Attach2,
`Attach3` = @Attach3,
`Attach4` = @Attach4,
`Attach5` = @Attach5,
`OperId` = @OperId,
`CreateTime` = @CreateTime,
`IsImage` = @IsImage,
`IsVideo` = @IsVideo,
`IsAttach` = @IsAttach,
`IsClass` = @IsClass,
`IsLearning` = @IsLearning,
`IsText` = @IsText,
`ViewCount` = @ViewCount,
`ReviewId` = @ReviewId,
`CanStudy` = @CanStudy,
`ImageUrl` = @ImageUrl
WHERE `NewsId` = @NewsId;

 ", mod);
        }

        public int CreateNews(TB_NEWS mod)
        {
            return dbManager.Excuted(@"INSERT INTO `eagles`.`tb_news`
(`OrgId`,
`NewsId`,
`ShortDesc`,
`Title`,
`HtmlContent`,
`Author`,
`Source`,
`Module`,
`Status`,
`BeginTime`,
`EndTime`,
`TestId`,
`Attach1`,
`Attach2`,
`Attach3`,
`Attach4`,
`Attach5`,
`OperId`,
`CreateTime`,
`IsImage`,
`IsVideo`,
`IsAttach`,
`IsClass`,
`IsLearning`,
`IsText`,
`ViewCount`,
`ReviewId`,
`CanStudy`,
`ImageUrl`)
VALUES
(@OrgId,
@NewsId,
@ShortDesc,
@Title,
@HtmlContent,
@Author,
@Source,
@Module,
@Status,
@BeginTime,
@EndTime,
@TestId,
@Attach1,
@Attach2,
@Attach3,
@Attach4,
@Attach5,
@OperId,
@CreateTime,
@IsImage,
@IsVideo,
@IsAttach,
@IsClass,
@IsLearning,
@IsText,
@ViewCount,
@ReviewId,
@CanStudy,
@ImageUrl);

", mod);

        }
    }
}
