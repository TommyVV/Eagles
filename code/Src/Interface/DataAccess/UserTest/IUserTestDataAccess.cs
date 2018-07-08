using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Question;

namespace Eagles.Interface.DataAccess.UserTest
{
    public interface IUserTestDataAccess : IInterfaceBase
    {
        List<TbQuestEx> GetTestPaper(int testId);

        TbTestPaper GetTestPaperInfo(int testId);

        List<TbQuestAnswer> GetTestRightAnswer(int testId, int isRight);

        TbUserTest GetUserTest(int testId, int userId);

        int CreateUserTest(TbUserTest userTest);

        int WriteAnswerCount(List<TbQuestAnswer> answers);

        int UpdateTestPaperUserCount(TbTestPaper testPaper);
    }
}