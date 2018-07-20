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
            return dbManager.Excuted(@"insert into eagles.tb_user_news (OrgId,BranchId,NewsId,UserId,ToUser,Title,HtmlContent,NewsType,Status,CreateTime,ViewsCount,RewardsScore,ReviewId,OrgReview,BranchReview,IsPublic,PublicTime)
values (@OrgId,@BranchId,@NewsId,@UserId,@ToUser,@Title,@HtmlContent,@NewsType,@Status,@CreateTime,@ViewsCount,@RewardsScore,@ReviewId,@OrgReview,@BranchReview,@IsPublic,@PublicTime) ", userNews);
        }

        public List<TbUserNews> GetUserNews(int userId, int pageIndex = 1, int pageSize = 10)
        {
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            var userNews = dbManager.Query<TbUserNews>(@"select OrgId,BranchId,NewsId,UserId,ToUser,Title,HtmlContent,NewsType,Status,CreateTime,ViewsCount,RewardsScore,ReviewId,IsPublic,PublicTime
OrgReview,BranchReview from eagles.tb_user_news where UserId = @UserId and Status = 0 limit @PageIndex, @PageSize ", 
new { UserId =  userId , PageIndex = pageIndexParameter, PageSize = pageSize });
            return userNews;
        }

        public List<TbUserNews> GetPblicUserNews(int userId, int pageIndex = 1, int pageSize = 10)
        {
            int pageIndexParameter = (pageIndex - 1) * pageSize;
            var userNews = dbManager.Query<TbUserNews>(@"select OrgId,BranchId,NewsId,UserId,ToUser,Title,HtmlContent,NewsType,Status,CreateTime,ViewsCount,RewardsScore,ReviewId,IsPublic,PublicTime
OrgReview,BranchReview from eagles.tb_user_news where Status = 0 and IsPublic = 0 and OrgReview = 0 and BranchReview = 0 limit @PageIndex, @PageSize ",
                new { UserId = userId, PageIndex = pageIndexParameter, PageSize = pageSize });
            return userNews;
        }
    }
}