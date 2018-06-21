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
            return dbManager.Query<TbNews>(@"SELECT OrgId,NewsId,ShortDesc,Title,HtmlContent,Author,Source,Module,Status,BeginTime,EndTime,TestId,Attach1,Attach2,Attach3,Attach4,Attach5,
OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrl FROM eagles.tb_news where Module=@Module And OrgId=@OrgId limit @Count",new {Module = moduleId, OrgId = appId, Count = count});

        }

        public TbNews GetNewsDetail(int newsId, int appId)
        {
            return dbManager.QuerySingle<TbNews>(@"SELECT OrgId,NewsId,ShortDesc,Title,HtmlContent,Author,Source,Module,Status,BeginTime,EndTime,TestId,Attach1,Attach2,Attach3,Attach4,Attach5,
OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrlFROM eagles.tb_news where NewsId=@NewsId And OrgId=@OrgId", new { NewsId = newsId, OrgId = appId });
        }

        public TbNews GetNewsTest(int testId)
        {
            return dbManager.QuerySingle<TbNews>(@"select c.TestId,a.QuestionId,a.Question,a.Multiple,a.MultipleCount,b.AnswerId,b.Answer,b.AnswerType,b.IsRight,b.ImageUrl
from eagles.tb_question a join eagles.tb_quest_anwser b on a.questionId = b.questionId join eagles.tb_test_question c on a.questionId = c.questionId
where c.TestId = @TestId ", new {TestId = testId});
        }
    }
}