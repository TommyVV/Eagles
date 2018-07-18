using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Organization.Model;
using Eagles.Application.Model.SMS.Model;
using Eagles.Application.Model.SMS.Request;
using Eagles.Application.Model.SMS.Response;
using Eagles.Base;
using Eagles.DomainService.Model.Config;
using Eagles.DomainService.Model.Org;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class SmsConfigHandler : ISmsConfigHandler
    {
        private readonly ISmsConfigDataAccess dataAccess;


        public SmsConfigHandler(ISmsConfigDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool EditSMS(EditSMSRequset requset)
        {
            SmsConfig mod;
            var now = DateTime.Now;
            if (requset.SmsInfo.VendorId > 0)
            {
                mod = new SmsConfig
                {
                    AppId = requset.SmsInfo.AppId,
                    AppKey = requset.SmsInfo.AppKey,
                    CreateTime = Convert.ToDateTime(requset.SmsInfo.CreateTime),
                    MaxCount = requset.SmsInfo.MaxCount,
                    Priority = requset.SmsInfo.Priority,
                    SendCount = requset.SmsInfo.SendCount,
                    ServiceUrl = requset.SmsInfo.ServiceUrl,
                    SginKey = requset.SmsInfo.SginKey,
                    Status = requset.SmsInfo.Status,
                    VendorId = requset.SmsInfo.VendorId,
                    VendorName = requset.SmsInfo.VendorName
                };

                return dataAccess.EditSMS(mod) > 0;
            }
            else
            {
                mod = new SmsConfig
                {
                    AppId = requset.SmsInfo.AppId,
                    AppKey = requset.SmsInfo.AppKey,
                    CreateTime = Convert.ToDateTime(requset.SmsInfo.CreateTime),
                    MaxCount = requset.SmsInfo.MaxCount,
                    Priority = requset.SmsInfo.Priority,
                    SendCount = requset.SmsInfo.SendCount,
                    ServiceUrl = requset.SmsInfo.ServiceUrl,
                    SginKey = requset.SmsInfo.SginKey,
                    Status = requset.SmsInfo.Status,
                    VendorId = requset.SmsInfo.VendorId,
                    VendorName = requset.SmsInfo.VendorName
                };
                return dataAccess.CreateSMS(mod) > 0;

            }

        }

        public bool RemoveSMS(RemoveSMSRequset requset)
        {
            return dataAccess.RemoveSMS(requset)>0;
        }

        public GetSMSResponse SMS(GetSMSRequset requset)
        {

            var response = new GetSMSResponse
            {
                TotalCount = 0,

            };
            List<SmsConfig> list = dataAccess.GetSMS(requset,out int totalCount) ?? new List<SmsConfig>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.List = list.Select(x => new SMSInfo
            {
                AppId = x.AppId,
                AppKey = x.AppKey,
                CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
                MaxCount = x.MaxCount,
                Priority = x.Priority,
                SendCount = x.SendCount,
                ServiceUrl = x.ServiceUrl,
                SginKey = x.SginKey,
                Status = x.Status,
                VendorId = x.VendorId,
                VendorName = x.VendorName
            }).ToList();
            return response;
        }

        public GetSMSDetailResponse GetSMSDetail(GetSMSDetailRequset requset)
        {
            var response = new GetSMSDetailResponse
            {

            };
            SmsConfig detail = dataAccess.GetSMSDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new SMSInfo
            {
                AppId = detail.AppId,
                AppKey = detail.AppKey,
                CreateTime = detail.CreateTime.ToString("yyyy-MM-dd"),
                MaxCount = detail.MaxCount,
                Priority = detail.Priority,
                SendCount = detail.SendCount,
                ServiceUrl = detail.ServiceUrl,
                SginKey = detail.SginKey,
                Status = detail.Status,
                VendorId = detail.VendorId,
                VendorName = detail.VendorName
            };
            return response;           
        }
    }
}
