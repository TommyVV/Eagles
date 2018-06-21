using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.Core.DataBase.UserArticle
{
    public interface IArticleDataAccess: IInterfaceBase
    {
        List<TbUserNews> GetUserNewsList(int userId);
    }
}
