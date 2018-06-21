using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.Interface.DataAccess.Menu;

namespace Ealges.DomianService.DataAccess.AppMenu
{
    public class MenuDataAccess: IMenuDataAccess
    {
        private readonly IDbManager dbManager;

        public MenuDataAccess(IDbManager dbManager)
        {
            this.dbManager = dbManager;
        }

        public List<Eagles.DomainService.Model.App.AppMenu> GetAppMenus(int appId)
        {
            var result=dbManager.Query<Eagles.DomainService.Model.App.AppMenu>(@"SELECT OrgId,
                MenuId,
                MenuName,
                Level,
                ParentMenuId,
                TragetUrl
            FROM eagles.tb_app_menu where OrgId=@OrgId ", new {OrgId = appId});
            return result;
        }
    }
}
