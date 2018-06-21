using System.Collections.Generic;
using Eagles.Base;
using Eagles.DomainService.Model.App;

namespace Eagles.Interface.DataAccess.Menu
{
    public interface IMenuDataAccess : IInterfaceBase
    {
        List<AppMenu> GetAppMenus(int appId);
    }
}