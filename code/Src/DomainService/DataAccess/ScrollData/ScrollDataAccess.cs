using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.Core.DataBase.ScrollAccess;

namespace Ealges.DomianService.DataAccess.ScrollData
{
    public class ScrollDataAccess: IScrollAccess
    {
        private readonly IDbManager dbManager;

        public ScrollDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<Eagles.DomainService.Model.ScrollImage.ScrollImage> GetScrollImg()
        {
            return dbManager.Query<Eagles.DomainService.Model.ScrollImage.ScrollImage>("select OrgId,PageType,ImageUrl from eagles.tb_scroll_image ", new { });
        }

        public List<Eagles.DomainService.Model.News.SystemNews> GetScrollNews()
        {
            return dbManager.Query<Eagles.DomainService.Model.News.SystemNews>("select OrgId,PageType,ImageUrl from eagles.tb_system_news ", new { });
        }
    }
}