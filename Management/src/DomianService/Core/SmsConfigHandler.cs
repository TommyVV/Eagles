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
            if (requset.Info.VendorId > 0)
            {
                mod = new SmsConfig
                {
                    AppId = requset.Info.AppId,
                    AppKey = requset.Info.AppKey,
                    CreateTime = Convert.ToDateTime(requset.Info.CreateTime),
                    MaxCount = requset.Info.MaxCount,
                    Priority = requset.Info.Priority,
                    SendCount = requset.Info.SendCount,
                    ServiceUrl = requset.Info.ServiceUrl,
                    SginKey = requset.Info.SginKey,
                    Status = requset.Info.Status,
                    VendorId = requset.Info.VendorId,
                    VendorName = requset.Info.VendorName
                };

                return dataAccess.EditSMS(mod) > 0;
            }
            else
            {
                mod = new SmsConfig
                {
                    AppId = requset.Info.AppId,
                    AppKey = requset.Info.AppKey,
                    CreateTime = Convert.ToDateTime(requset.Info.CreateTime),
                    MaxCount = requset.Info.MaxCount,
                    Priority = requset.Info.Priority,
                    SendCount = requset.Info.SendCount,
                    ServiceUrl = requset.Info.ServiceUrl,
                    SginKey = requset.Info.SginKey,
                    Status = requset.Info.Status,
                    VendorId = requset.Info.VendorId,
                    VendorName = requset.Info.VendorName
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

            response.List = list.Select(x => new SMS
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

            response.Info = new SMS
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
