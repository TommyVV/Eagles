using Eagles.Base;
using Eagles.Base.Config;

namespace Eagles.Interface.Configuration
{
    public interface IDataBaseConfiguration:IInterfaceBase
    {
        DataBaseConfig DBconfig { get; }
    }
}
