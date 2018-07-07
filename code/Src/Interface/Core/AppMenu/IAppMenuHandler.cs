using Eagles.Base;
using Eagles.Application.Model.GetMenu;

namespace Eagles.Interface.Core.AppMenu
{
    public interface IAppMenuHandler:IInterfaceBase
    {
        GetMenuResponse GetMenu(GetMenuRequest request);
    }
}