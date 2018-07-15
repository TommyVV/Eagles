using System.Collections.Generic;
using Eagles.Application.Model.News.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.DataAccess
{
    public interface INewsDataAccess : IInterfaceBase
    {
        List<TbNews> GetNewsList(GetNewRequset requset, out int totalCount);
        TbNews GetNewsDetail(GetNewDetailRequset requset);
        int RemoveNews(RemoveNewRequset requset);
        int EditNews(TbNews mod);
        int CreateNews(TbNews mod);
        int ImportNews(List<TbNews> mod);
    }
}
