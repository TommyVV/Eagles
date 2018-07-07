using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.User;
using Eagles.Interface.DataAccess.UserArticle;

namespace Ealges.DomianService.DataAccess.UserArticle
{
    public class ArticleDataAccess : IArticleDataAccess
    {
        private readonly IDbManager dbManager;

        public ArticleDataAccess(IDbManager dbManager)
        {
           this.dbManager = dbManager;
        }

        public int CreateArticle(TbUserNews userNews)
        {
            return dbManager.Excuted(@"insert into eagles.tb_user_news (OrgId,BranchId,NewsId,UserId,Title,HtmlContent,NewsType,Status,CreateTime,ViewsCount,RewardsScore,ReviewId,OrgReview,BranchReview)
values (@OrgId,@BranchId,@NewsId,@UserId,@Title,@HtmlContent,@NewsType,@Status,@CreateTime,@ViewsCount,@RewardsScore,@ReviewId,@OrgReview,@BranchReview) ", userNews);
        }

        public List<TbUserNews> GetUserNewsList(int userId)
        {
            var userNews = dbManager.Query<TbUserNews>(@"select OrgId,BranchId,NewsId,UserId,Title,HtmlContent,NewsType,Status,CreateTime,ViewsCount,RewardsScore,ReviewId,
OrgReview,BranchReview from eagles.tb_user_news where UserId=@UserId", new { UserId =  userId  });
            return userNews;
        }
    }
}