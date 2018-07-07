using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.ScrollAccess;

namespace Ealges.DomianService.DataAccess.ScrollData
{
    public class ScrollDataAccess: IScrollAccess
    {
        private readonly IDbManager dbManager;

        public ScrollDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<Eagles.DomainService.Model.ScrollImage.TbScrollImage> GetScrollImg(string pageType)
        {
            return dbManager.Query<Eagles.DomainService.Model.ScrollImage.TbScrollImage>("select OrgId,PageType,ImageUrl,TargetUrl from eagles.tb_scroll_image where PageType = @PageType", new {PageType = pageType});
        }

        public List<Eagles.DomainService.Model.News.TbSystemNews> GetScrollNews(string nowDate,string date)
        {
            return dbManager.Query<Eagles.DomainService.Model.News.TbSystemNews>(
                "select newsId,newsName,newsContent,htmlDesc  from tb_system_news where NoticeTime=@NoticeTime or NoticeTime=@Date ",
                new {NoticeTime = nowDate, Date = date});
        }
    }
}