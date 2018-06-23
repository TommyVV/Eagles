using System;
using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.Question;
using Eagles.DomainService.Model.RewardScore;
using Eagles.DomainService.Model.User;
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
OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrl from eagles.tb_news where Module=@Module And OrgId=@OrgId limit @Count",new {Module = moduleId, OrgId = appId, Count = count});
        }

        public TbNews GetNewsDetail(int newsId, int appId)
        {
            return dbManager.QuerySingle<TbNews>(@"SELECT OrgId,NewsId,ShortDesc,Title,HtmlContent,Author,Source,Module,Status,BeginTime,EndTime,TestId,Attach1,Attach2,Attach3,Attach4,Attach5,
OperId,CreateTime,IsImage,IsVideo,IsAttach,IsClass,IsLearning,IsText,ViewCount,ReviewId,CanStudy,ImageUrl from eagles.tb_news where NewsId=@NewsId And OrgId=@OrgId", new { NewsId = newsId, OrgId = appId });
        }

        public List<TbQuestEx> GetNewsTest(int testId)
        {
            return dbManager.Query<TbQuestEx>(@"select c.TestId,a.QuestionId,a.Question,a.Multiple,a.MultipleCount,b.AnswerId,b.Answer,b.AnswerType,b.IsRight,b.ImageUrl
from eagles.tb_question a join eagles.tb_quest_anwser b on a.questionId = b.questionId join eagles.tb_test_question c on a.questionId = c.questionId
where c.TestId = @TestId ", new {TestId = testId});
        }

        public TbTestPaper GetTestPaperInfo(int testId)
        {
            return dbManager.QuerySingle<TbTestPaper>(@"select TestId,TestName,HasReward,QuestionSocre,PassScore,HasLimitedTime,LimitedTime,HtmlDescription where TestId=@TestId And Status=0 ", new {NewsId = testId});
        }

        public List<TbQuestEx> GetTestRightAnswer(int testId)
        {
            return dbManager.Query<TbQuestEx>(@"select c.TestId,a.QuestionId,a.Question,a.Multiple,a.MultipleCount,b.AnswerId,b.Answer,b.AnswerType,b.IsRight,b.ImageUrl
from eagles.tb_question a join eagles.tb_quest_anwser b on a.questionId = b.questionId join eagles.tb_test_question c on a.questionId = c.questionId
where c.TestId = @TestId and b.IsRight = 1; ", new { TestId = testId });
        }

        public int CreateUserTest(TbUserTest userTest)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_test (OrgId,BranchId,UserId,TestId,Score,TotalScore,CreateTime,UseTime) value (@OrgId,@BranchId,@UserId,@TestId,@Score,@TotalScore,@CreateTime,@UseTime) ",
                new
                {
                    OrgId = userTest.OrgId,
                    BranchId = userTest.BranchId,
                    UserId = userTest.UserId,
                    TestId = userTest.TestId,
                    Score = userTest.Score,
                    TotalScore = userTest.TotalScore,
                    CreateTime = DateTime.Now,
                    UseTime = userTest.UseTime
                });
        }
    }
}