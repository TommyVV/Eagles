using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.DataAccess.NewsDA
{
    public interface INewsDA:IInterfaceBase
    {
        List<News> GetModuleNews(int moduleId,int appId,int count);

        News GetNewsDetail(int newsId, int appId);
    }
}
