using System.Linq;
using System.Collections.Generic;
using Eagles.Base;
using Eagles.Interface.Core.AppMenu;
using Eagles.Interface.DataAccess.Menu;
using Eagles.Interface.DataAccess.Util;
using Eagles.Application.Model.GetMenu;

namespace Eagles.DomainService.Core.AppMenu
{
    public class AppMenuHandler : IAppMenuHandler
    {
        private readonly IMenuDataAccess menuDataAccess;

        private readonly IUtil util;

        public AppMenuHandler(IMenuDataAccess menuDataAccess, IUtil util)
        {
            this.menuDataAccess = menuDataAccess;
            this.util = util;
        }

        public GetMenuResponse GetMenu(GetMenuRequest request)
        {
            var response = new GetMenuResponse();
            if (request.AppId <= 0)
                throw new TransactionException("01", "AppId不允许为空");
            if (util.CheckAppId(request.AppId))
                throw new TransactionException("01", "AppId不存在");
            var menus = menuDataAccess.GetAppMenus(request.AppId);
            if (menus == null || !menus.Any())
            {
                return response;
            }
            var mainMenu = menus.Where(x => x.Level == "1").Select(x => new Application.Model.GetMenu.AppMenu
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName,
                TargetUrl = x.TargetUrl,
                SubMenus = new List<AppSubMenu>(),
                HasSubMenu = false

            }).ToList();
            var secondMenu = menus.Where(x => x.Level == "2").Select(x => new AppSubMenu()
            {
                MenuId = x.MenuId,
                MenuName = x.MenuName,
                TargetUrl = x.TargetUrl,
                ParentMenuId = x.ParentMenuId,
            }).ToList();
            // build relations
            mainMenu.ForEach(x =>
            {
                var sub = secondMenu.FindAll(y => y.ParentMenuId == x.MenuId).ToList();
                if (sub.Any())
                {
                    x.SubMenus = sub;
                    x.HasSubMenu = true;
                }
            });
            response.AppMenus = mainMenu;
            return response;
        }
    }
}