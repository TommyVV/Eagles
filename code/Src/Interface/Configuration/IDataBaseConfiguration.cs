using Eagles.Base;
using Eagles.DomainService.Model.Config;

namespace Eagles.Interface.Configuration
{
    public interface IDataBaseConfiguration:IInterfaceBase
    {
        DataBaseConfig DBconfig { get; }
    }
}
