using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.DataAccess.NewsDa
{
    public interface INewsDa : IInterfaceBase
    {
        List<TbNews> GetModuleNews(int moduleId,int appId,int count);

        TbNews GetNewsDetail(int newsId, int appId);

        TbNews GetNewsTest(int testId);
    }
}