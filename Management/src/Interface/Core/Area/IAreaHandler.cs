using Eagles.Application.Model.Area;
using Eagles.Base;

namespace Eagles.Interface.Core.Area
{
    public interface IAreaHandler:IInterfaceBase
    {
        AreaResponse Process();
    }
}
