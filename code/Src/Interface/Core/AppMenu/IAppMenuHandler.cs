using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.GetMenu;
using Eagles.Base;

namespace Eagles.Interface.Core.AppMenu
{
    public interface IAppMenuHandler:IInterfaceBase
    {
        GetMenuResponse Process(GetMenuRequest request);
    }
}
