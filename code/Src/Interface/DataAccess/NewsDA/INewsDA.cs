using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.DataAccess.NewsDa
{
    public interface INewsDa : IInterfaceBase
    {
        List<TB_NEWS> GetModuleNews(int moduleId,int appId,int count);

        TB_NEWS GetNewsDetail(int newsId, int appId);
    }
}