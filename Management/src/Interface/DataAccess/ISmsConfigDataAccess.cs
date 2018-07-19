using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.SMS.Request;
using Eagles.Application.Model.SMSOrg.Request;
using Eagles.Base;
using Eagles.DomainService.Model.Config;
using Eagles.DomainService.Model.Org;

namespace Eagles.Interface.DataAccess
{
    public interface ISmsConfigDataAccess: IInterfaceBase
    {
        int EditSMS(SmsConfig mod);
        int CreateSMS(SmsConfig mod);
        int RemoveSMS(RemoveSMSRequset requset);
        List<SmsConfig> GetSMS(GetSMSRequset requset, out int totalCount);
        SmsConfig GetSMSDetail(GetSMSDetailRequset requset);
        int EditOrgSmsConfig(TbOrgSmsConfig mod);
        int CreateOrgSmsConfig(TbOrgSmsConfig mod);
       
        TbOrgSmsConfig GetSMSOrgDetail(GetSMSOrgDetailRequset requset);
        List<TbOrgSmsConfig> GetSMSOrg(GetSMSOrgRequest requset, out int i);
        int RemoveOrgSmsConfig(RemoveSMSOrgRequest requset);
    }
}
