using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.User;
using Eagles.DomainService.Model.Question;

namespace Eagles.Interface.DataAccess.NewsDa
{
    public interface INewsDa : IInterfaceBase
    {
        List<TbNews> GetModuleNews(int moduleId,int appId,int count);

        TbNews GetNewsDetail(int newsId, int appId);

        List<TbQuestEx> GetTestPaper(int testId);

        TbTestPaper GetTestPaperInfo(int testId);

        List<TbQuestAnswer> GetTestRightAnswer(int testId);

        int CreateUserTest(TbUserTest userTest);
    }
}