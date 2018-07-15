using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Base;
using Eagles.DomainService.Model.Config;

namespace Eagles.Interface.Configuration
{
    public interface IMenuConfiguration:IInterfaceBase
    {
        FunctionMenu FunctionMenu { get; }
    }
}
