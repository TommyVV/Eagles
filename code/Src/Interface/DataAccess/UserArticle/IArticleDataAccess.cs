using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.UserArticle
{
    public interface IArticleDataAccess : IInterfaceBase
    {
        int CreateArticle(TbUserNews userNews);

        List<TbUserNews> GetUserNews(int userId, int pageIndex, int pageSize);
        
        List<TbUserNews> GetPblicUserNews(int userId, int pageIndex, int pageSize);
    }
}