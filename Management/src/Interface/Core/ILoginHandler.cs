using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Login.Model;
using Eagles.Application.Model.Login.Requset;
using Eagles.Application.Model.Login.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface ILoginHandler : IInterfaceBase
    {
        LoginResponse Login(LoginRequset requset);

        string VerificationCode(Verification requset);
    }
}
