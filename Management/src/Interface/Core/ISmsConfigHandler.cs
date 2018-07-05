using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.SMS.Request;
using Eagles.Application.Model.SMS.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface ISmsConfigHandler : IInterfaceBase
    {
        bool EditSMS(EditSMSRequset requset);

        bool RemoveSMS(RemoveSMSRequset requset);

        GetSMSResponse SMS(GetSMSRequset requset);

        GetSMSDetailResponse GetSMSDetail(GetSMSDetailRequset requset);
    }
}
