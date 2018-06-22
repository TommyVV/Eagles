using System.Collections.Generic;
using System.Text;
using Dapper;
using Eagles.Application.Model.News.Requset;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.Interface.Core.DataBase;

namespace Ealges.DomianService.DataAccess
{
    public class NewsDataAccess : INewsDataAccess
    {
        private readonly IDbManager dbManager;

        public NewsDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbNews> GetNewsList(GetNewRequset requset)
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
FROM `eagles`.`tb_news`
  where  1=1  {0}  
 ", parameter);

            return dbManager.Query<TbNews>(sql.ToString(), dynamicParams);
        }

        public TbNews GetNewsDetail(GetNewDetailRequset requset)
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
FROM `eagles`.`tb_news`
  where NewsId=@NewsId;
 ");
            dynamicParams.Add("NewsId", requset.NewsId );

            return dbManager.QuerySingle<TbNews>(sql.ToString(), dynamicParams);

        }

        public int RemoveNews(RemoveNewRequset requset)
        {

            return dbManager.Excuted(@" DELETE FROM `eagles`.`tb_news` where
                `NewsId` = @NewsId;
", new {requset.NewsId});
        }

        public int EditNews(TbNews mod)
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

        public int CreateNews(TbNews mod)
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
