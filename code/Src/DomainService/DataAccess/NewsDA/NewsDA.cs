using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.Interface.DataAccess.NewsDa;

namespace Ealges.DomianService.DataAccess.NewsDA
{
    public class NewsDa: INewsDa
    {
        private readonly IDbManager dbManager;

        public NewsDa(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbNews> GetModuleNews(int moduleId,int appId,int count)
        {
            return dbManager.Query<TbNews>(@"SELECT `tb_news`.`OrgId`,
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
    `tb_news`.`ImageUrl`,
    `tb_news`.`IsExternal`,
    `tb_news`.`ExternalUrl`
FROM `eagles`.`tb_news`  where Module=@Module And OrgId=@OrgId limit @Count", new {Module = moduleId, OrgId = appId, Count = count});
        }

        public TbNews GetNewsDetail(int newsId, int appId)
        {
            return dbManager.QuerySingle<TbNews>(@"SELECT `tb_news`.`OrgId`,
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
    `tb_news`.`ImageUrl`,
    `tb_news`.`IsExternal`,
    `tb_news`.`ExternalUrl`
FROM `eagles`.`tb_news`  where NewsId=@NewsId And OrgId=@OrgId and Status = 0 ", new { NewsId = newsId, OrgId = appId });
        }
    }
}