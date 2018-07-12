using System.Collections.Generic;
using Eagles.Base.DataBase;
using Eagles.DomainService.Model.App;
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

        public List<TbAppMenu> GetAppMenus(int appId)
        {
            var result=dbManager.Query<TbAppMenu>(@"SELECT a.OrgId,a.MenuId,a.MenuName,a.Level,a.ParentMenuId,a.TargetUrl,b.Logo from eagles.tb_app_menu  a 
inner join eagles.tb_org_info b on a.orgId=b.orgId ", new {OrgId = appId});
            return result;
        }
    }
}