using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.UserArticle;

namespace Ealges.DomianService.DataAccess.UserArticle
{
    public class ArticleDataAccess: IArticleDataAccess
    {
        private readonly IDbManager dbManager;

        public ArticleDataAccess(IDbManager dbManager)
        {
           this.dbManager = dbManager;
        }

        public List<TbUserNews> GetUserNewsList(int userId)
        {

            var userNews = dbManager.Query<TbUserNews>(@"SELECT 
            OrgId,
            BranchId,
            NewsId,
            UserId,
            Title,
            HtmlContent,
            NewsType,
            Status,
            CreateTime,
            ViewsCount,
            RewardsScore,
            ReviewId,
            OrgReview,
            BranchReview
                FROM eagles.tb_user_news where userId=@userId", new { userId =  userId  });
            return userNews;

        }
    }
}
