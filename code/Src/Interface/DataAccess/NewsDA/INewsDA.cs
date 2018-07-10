using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.DataAccess.NewsDa
{
    public interface INewsDa : IInterfaceBase
    {
        List<TbNews> GetModuleNews(int moduleId,int appId, int pageIndex = 1, int pageSize = 10);

        TbNews GetNewsDetail(int newsId, int appId);

        int AddNewsViewCount(int newsId);
    }
}