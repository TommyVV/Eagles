using Eagles.Base;
using Eagles.DomainService.Model.Config;

namespace Eagles.Interface.Configuration
{
    public interface IEaglesConfig:IInterfaceBase
    {
        EaglesConfiguration EaglesConfiguration { get;}
    }
}
