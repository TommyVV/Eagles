using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.Exercises;

namespace Eagles.Interface.DataAccess.NewsDa
{
    public interface INewsDa : IInterfaceBase
    {
        List<TbNews> GetModuleNews(int moduleId,int appId,int count);

        TbNews GetNewsDetail(int newsId, int appId);

        List<TbQuestEx> GetNewsTest(int testId);
    }
}