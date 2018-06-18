using System.Collections.Generic;
using Eagles.Application.Model.News.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.Core.DataBase
{
    public interface INewsDataAccess : IInterfaceBase
    {
        List<TB_NEWS> GetNewsList(GetNewsRequset requset);
        TB_NEWS GetNewsDetail(GetNewsDetailRequset requset);
        int RemoveNews(RemoveNewsRequset requset);
        int EditNews(TB_NEWS mod);
        int CreateNews(TB_NEWS mod);
    }
}
