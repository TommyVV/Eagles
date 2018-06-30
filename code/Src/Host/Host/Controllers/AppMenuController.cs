using System.Web.Http;
using Eagles.Application.Model.GetMenu;
using Eagles.Interface.Core.AppMenu;

namespace Eagles.Application.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ValidServiceToken]
    public class AppMenuController: ApiController
    {
        private readonly IAppMenuHandler appMenu;

        /// <summary>
        /// construction
        /// </summary>
        /// <param name="appMenu"></param>
        public AppMenuController(IAppMenuHandler appMenu)
        {
            this.appMenu = appMenu;
        }

        /// <summary>
        /// 获取应用菜单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public GetMenuResponse GetAppMenu(GetMenuRequest request)
        {
            return appMenu.GetMenu(request);
        }
    }
}