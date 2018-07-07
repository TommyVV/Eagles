using System.Collections.Generic;
using Eagles.Application.Model.Test;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface ITestDemo: IInterfaceBase
    {
        List<AreaInfo> Test();

    }
}
