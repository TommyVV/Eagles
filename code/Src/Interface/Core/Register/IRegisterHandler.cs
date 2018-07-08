using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Register;
using Eagles.Base;

namespace Eagles.Interface.Core.Register
{
    public interface IRegisterHandler:IInterfaceBase
    {
        ValidateCodeResponse GenerateSmsCode(ValidateCodeRequest request);
    }
}
