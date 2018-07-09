using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess.UserArticle
{
    public interface IArticleDataAccess : IInterfaceBase
    {
        int CreateArticle(TbUserNews userNews);

        List<TbUserNews> GetUserNewsList(int userId, int pageIndex, int pageSize);
    }
}