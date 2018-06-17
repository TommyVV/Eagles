using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.Interface.DataAccess.NewsDA;

namespace Ealges.DomianService.DataAccess.NewsDA
{
    public class NewsDA: INewsDA
    {
        private readonly IDbManager dbManager;

        public NewsDA(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<News> GetModuleNews(int moduleId,int appId,int count)
        {
          return  dbManager.Query<News>(@"SELECT OrgId,
    NewsId,
    ShortDesc,
    Title,
    HtmlContent,
    Author,
    Source,
    Module,
    Status,
    BeginTime,
    EndTime,
    TestId,
    Attach1,
    Attach2,
    Attach3,
    Attach4,
    Attach5,
    OperId,
    CreateTime,
    IsImage,
    IsVideo,
    IsAttach,
    IsClass,
    IsLearning,
    IsText,
    ViewCount,
    ReviewId,
    CanStudy,
    ImageUrl
FROM eagles.tb_news where Module=@Module And OrgId=@OrgId  limit @count", new {Module = moduleId, OrgId =appId, count =count});

        }

        public News GetNewsDetail(string newsId, string appId)
        {
            return dbManager.QuerySingle<News>(@"SELECT OrgId,
    NewsId,
    ShortDesc,
    Title,
    HtmlContent,
    Author,
    Source,
    Module,
    Status,
    BeginTime,
    EndTime,
    TestId,
    Attach1,
    Attach2,
    Attach3,
    Attach4,
    Attach5,
    OperId,
    CreateTime,
    IsImage,
    IsVideo,
    IsAttach,
    IsClass,
    IsLearning,
    IsText,
    ViewCount,
    ReviewId,
    CanStudy,
    ImageUrl
FROM eagles.tb_news where NewsId=@NewsId And OrgId=@OrgId", new { NewsId = newsId, OrgId = appId });
        }
    }
}
