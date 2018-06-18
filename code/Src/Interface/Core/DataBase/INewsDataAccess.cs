using System.Collections.Generic;
using Eagles.Application.Model.News.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.Core.DataBase
{
    public interface INewsDataAccess : IInterfaceBase
    {
        List<TB_NEWS> GetNewsList(GetNewRequset requset);
        TB_NEWS GetNewsDetail(GetNewDetailRequset requset);
        int RemoveNews(RemoveNewRequset requset);
        int EditNews(TB_NEWS mod);
        int CreateNews(TB_NEWS mod);
    }
}
