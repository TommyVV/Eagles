using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Question;
using Eagles.Interface.DataAccess.UserTest;

namespace Ealges.DomianService.DataAccess.UserTest
{
    public class UserTestDataAccess : IUserTestDataAccess
    {
        private readonly IDbManager dbManager;

        public UserTestDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<TbQuestEx> GetTestPaper(int testId)
        {
            return dbManager.Query<TbQuestEx>(@"select c.TestId,a.QuestionId,a.Question,a.Multiple,a.MultipleCount,b.AnswerId,b.Answer,b.AnswerType,b.IsRight,b.ImageUrl
from eagles.tb_question a join eagles.tb_quest_anwser b on a.questionId = b.questionId join eagles.tb_test_question c on a.questionId = c.questionId where c.TestId = @TestId ", new { TestId = testId });
        }

        public TbTestPaper GetTestPaperInfo(int testId)
        {
            return dbManager.QuerySingle<TbTestPaper>(@"select TestId,TestName,HasReward,QuestionSocre,PassScore,HasLimitedTime,LimitedTime,HtmlDescription 
from eagles.tb_test_paper where TestId=@TestId And Status=0 ", new { TestId = testId });
        }

        public List<TbQuestAnswer> GetTestRightAnswer(int testId, int isRight)
        {
            return dbManager.Query<TbQuestAnswer>(@"select a.OrgId, a.QuestionId, a.AnswerId, a.Answer, a.AnswerType, a.IsRight, a.ImageUrl
from eagles.tb_quest_anwser a join eagles.tb_test_question b on a.questionId = b.questionId
where b.TestId = @TestId and a.IsRight = @IsRight; ", new {TestId = testId, IsRight = isRight});
        }

        public TbUserTest GetUserTest(int testId,int userId)
        {
            return dbManager.QuerySingle<TbUserTest>("select TestId from eagles.tb_user_test where TestId = @TestId and UserId = @UserId", new {TestId = testId, UserId = userId});
        }

        public int CreateUserTest(TbUserTest userTest)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_test (OrgId,BranchId,UserId,TestId,Score,TotalScore,CreateTime,UseTime) 
value (@OrgId,@BranchId,@UserId,@TestId,@Score,@TotalScore,@CreateTime,@UseTime) ", userTest);
        }
    }
}