using System.Collections.Generic;
using System.Linq;
using Eagles.Base.DataBase;
using Eagles.Base.DataBase.Modle;
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
            return dbManager.Query<TbQuestEx>(@"select a.TestId,a.QuestionSocre,a.PassScore,a.PassAwardScore,a.LimitedTime,c.QuestionId,c.Question
,c.Multiple,c.MultipleCount,d.AnswerId,a.TestName,a.HtmlDescription,
d.Answer,d.AnswerType,d.IsRight,d.ImageUrl ,a.UserCount
from eagles.tb_test_paper a
join eagles.tb_test_question b on b.TestId = a.TestId
join eagles.tb_question c on c.QuestionId = b.QuestionId
join eagles.tb_quest_anwser d on d.QuestionId = c.QuestionId 
where a.TestId = @TestId ", new { TestId = testId });
        }

        public TbTestPaper GetTestPaperInfo(int testId)
        {
            return dbManager.QuerySingle<TbTestPaper>(@"select TestId,TestName,HasReward,QuestionSocre,PassScore,HasLimitedTime,LimitedTime,HtmlDescription ,UserCount, TestType
from eagles.tb_test_paper where TestId=@TestId And Status=0 ", new { TestId = testId });
        }

        public List<TbQuestAnswer> GetTestRightAnswer(int testId, int isRight)
        {
            return dbManager.Query<TbQuestAnswer>(@"select a.OrgId, a.QuestionId, a.AnswerId, a.Answer, a.AnswerType, a.IsRight, a.ImageUrl
from eagles.tb_quest_anwser a join eagles.tb_test_question b on a.questionId = b.questionId
where b.TestId = @TestId and a.IsRight = @IsRight; ", new { TestId = testId, IsRight = isRight });
        }

        public TbUserTest GetUserTest(int testId, int userId)
        {
            return dbManager.QuerySingle<TbUserTest>("select TestId from eagles.tb_user_test where TestId = @TestId and UserId = @UserId", new { TestId = testId, UserId = userId });
        }

        public int CreateUserTest(TbUserTest userTest)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_test (OrgId,BranchId,UserId,TestId,Score,TotalScore,CreateTime,UseTime,Answer) 
value (@OrgId,@BranchId,@UserId,@TestId,@Score,@TotalScore,@CreateTime,@UseTime,@Answer) ", userTest);
        }

        public int WriteAnswerCount(List<TbQuestAnswer> answers)
        {
            var command = @" update tb_quest_anwser set UserCount=UserCount+1 where AnswerId in (@AnswerId)";
            return dbManager.Excuted(command, answers);
        }

        public int UpdateTestPaperUserCount(TbTestPaper testPaper)
        {
            var command = @" update tb_test_paper set UserCount=UserCount+1 where TestId = (@TestId)";
            return dbManager.Excuted(command, testPaper);
        }
    }
}