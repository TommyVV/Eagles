using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Organization.Model;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.SMS.Model;
using Eagles.Application.Model.SMS.Request;
using Eagles.Application.Model.SMS.Response;
using Eagles.Application.Model.SMSOrg.Model;
using Eagles.Application.Model.SMSOrg.Request;
using Eagles.Application.Model.SMSOrg.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.Config;
using Eagles.DomainService.Model.Org;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class SmsConfigHandler : ISmsConfigHandler
    {
        private readonly ISmsConfigDataAccess dataAccess;

        private readonly ICacheHelper cacheHelper;

        private readonly IOrganizationDataAccess OrgdataAccess;

        public SmsConfigHandler(ISmsConfigDataAccess dataAccess, ICacheHelper cacheHelper, IOrganizationDataAccess orgdataAccess)
        {
            this.dataAccess = dataAccess;
            this.cacheHelper = cacheHelper;
            OrgdataAccess = orgdataAccess;
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
            return dataAccess.RemoveSMS(requset) > 0;
        }

        public GetSMSResponse SMS(GetSMSRequset requset)
        {

            var response = new GetSMSResponse
            {
                TotalCount = 0,

            };
            List<SmsConfig> list = dataAccess.GetSMS(requset, out int totalCount) ?? new List<SmsConfig>();

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

        public bool EditOrgSmsConfig(EditSMSOrgRequset request)
        {
            var tokenInfo = cacheHelper.GetData<TbUserToken>(request.Token);

            TbOrgSmsConfig mod;
            var now = DateTime.Now;
            //查询
            var exists=dataAccess.GetSMSOrgDetail(new GetSMSOrgDetailRequset()
            {
                OrgId = request.Info.OrgId,
                VendorId = request.Info.VendorId
            });

            if (exists!=null&& exists.OrgId>0)
            {
                mod = new TbOrgSmsConfig()
                {

                    //CreateTime = Convert.ToDateTime(request.SmsInfo.CreateTime),
                    MaxCount = request.Info.MaxCount,
                    Priority = request.Info.Priority,
                    SendCount = request.Info.SendCount,
                    OperId = tokenInfo.UserId,
                    OrgId = request.Info.OrgId,
                    Status = request.Info.Status,
                    VendorId = request.Info.VendorId,

                };

                return dataAccess.EditOrgSmsConfig(mod) > 0;
            }
            else
            {
                mod = new TbOrgSmsConfig()
                {

                    //CreateTime = Convert.ToDateTime(request.SmsInfo.CreateTime),
                    MaxCount = request.Info.MaxCount,
                    Priority = request.Info.Priority,
                    SendCount = request.Info.SendCount,
                    OperId = tokenInfo.UserId,
                    OrgId = request.Info.OrgId,
                    Status = request.Info.Status,
                    VendorId = request.Info.VendorId,

                };
                return dataAccess.CreateOrgSmsConfig(mod) > 0;

            }

        }

        public GetSMSOrgResponse GetSMSOrg(GetSMSOrgRequest requset)
        {

            var response = new GetSMSOrgResponse
            {
                TotalCount = 0,

            };
            List<TbOrgSmsConfig> list = dataAccess.GetSMSOrg(requset, out int totalCount) ?? new List<TbOrgSmsConfig>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            var orginfo = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;

            response.List = list.Select(x => new SMSOrg()
            {

                CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
                MaxCount = x.MaxCount,
                Priority = x.Priority,
                SendCount = x.SendCount,
                VendorName = x.VendorName,
                Status = x.Status,
                VendorId = x.VendorId,
                OrgId = x.OrgId,
                OrgName = orginfo.FirstOrDefault(f => f.OrgId == x.OrgId)?.OrgName
            }).ToList();
            return response;
        }

        public bool RemoveOrgSmsConfig(RemoveSMSOrgRequest requset)
        {

            return dataAccess.RemoveOrgSmsConfig(requset) > 0;
        }

        public GetSMSOrgDetailResponse GetSMSOrgDetail(GetSMSOrgDetailRequset requset)
        {

            var response = new GetSMSOrgDetailResponse
            {

            };
            TbOrgSmsConfig detail = dataAccess.GetSMSOrgDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            var orginfo = OrgdataAccess.GetOrganizationDetail(new GetOrganizationDetailRequset()
            {
                OrgId = requset.OrgId
            });

            response.Info = new SMSOrg
            {
                CreateTime = detail.CreateTime.ToString("yyyy-MM-dd"),
                MaxCount = detail.MaxCount,
                Priority = detail.Priority,
                SendCount = detail.SendCount,
                Status = detail.Status,
                VendorId = detail.VendorId,
                OrgId = detail.OrgId,
                OrgName = orginfo.OrgName
            };
            return response;
        }
    }
}
