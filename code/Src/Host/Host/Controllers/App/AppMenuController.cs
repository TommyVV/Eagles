using System.Web.Http;
using Eagles.Application.Model.AppModel.GetMenu;
using Eagles.Interface.Core.AppMenu;

namespace Eagles.Application.Host.Controllers.App
{
    public class AppMenuController: ApiController
    {
        private readonly IAppMenuHandler appMenu;

        public AppMenuController(IAppMenuHandler appMenu)
        {
            this.appMenu = appMenu;
        }

        [Route("api/app/menu")]
        [HttpPost]
        public GetMenuResponse GetAppMenu(GetMenuRequest request)
        {
            return appMenu.Process(request);
        }
    }
}