using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Application.Model.Organization.Model;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.Organization.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.Base.Utility;
using Eagles.DomainService.Model.Org;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class OrganizationHandler : IOrganizationHandler
    {
        private readonly IOrganizationDataAccess dataAccess;

        private readonly ICacheHelper Cache;
        public OrganizationHandler(IOrganizationDataAccess dataAccess, ICacheHelper cache)
        {
            this.dataAccess = dataAccess;
            Cache = cache;
        }

        public bool EditOrganization(EditOrganizationRequset requset)
        {

            var tokenInfo = Cache.GetData<TbUserToken>(requset.Token);

            TbOrgInfo mod;
            var now = DateTime.Now;
            if (requset.Info.OrgId > 0)
            {
                mod = new TbOrgInfo
                {
                    Address = requset.Info.Address,
                    Province = requset.Info.Province,
                    OrgName = requset.Info.OrgName,
                    City = requset.Info.City,
                    District = requset.Info.District,
                    EditTime = now,
                    Logo = requset.Info.Logo,
                    OperId = tokenInfo.UserId,
                    OrgId = requset.Info.OrgId
                };

                return dataAccess.EditOrganization(mod) > 0;


            }
            else
            {
                mod = new TbOrgInfo
                {
                    Address = requset.Info.Address,
                    Province = requset.Info.Province,
                    OrgName = requset.Info.OrgName,
                    City = requset.Info.City,
                    CreateTime = now,
                    District = requset.Info.District,
                    //EditTime = now,
                    Logo = requset.Info.Logo,
                    OperId = tokenInfo.UserId,

                };
                return dataAccess.CreateOrganization(mod) > 0;


            }

        }

        public bool RemoveOrganization(RemoveOrganizationRequset requset)
        {
            return dataAccess.RemoveOrganization(requset) > 0;
        }

        public GetOrganizationResponse Organization(GetOrganizationRequset requset)
        {
            var response = new GetOrganizationResponse
            {
                TotalCount = 0,

            };
            List<TbOrgInfo> list = dataAccess.GetOrganizationList(requset, out int totalCount) ?? new List<TbOrgInfo>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.TotalCount = totalCount;
            response.List = list.Select(x => new Organization
            {
                Address = x.Address,
                City = x.City,
                CreateTime = x.CreateTime.FormartDatetime(),
                District = x.District,
                OrgId = x.OrgId,
                OrgName = x.OrgName,
                Province = x.Province,
            }).ToList();
            return response;
        }

        public GetOrganizationDetailResponse GetOrganizationDetail(GetOrganizationDetailRequset requset)
        {
            var response = new GetOrganizationDetailResponse
            {

            };
            TbOrgInfo detail = dataAccess.GetOrganizationDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new OrganizationDetail
            {
                Address = detail.Address,
                City = detail.City,
                CreateTime = detail.CreateTime.FormartDatetime(),
                District = detail.District,
                OrgId = detail.OrgId,
                OrgName = detail.OrgName,
                Province = detail.Province,
                Logo = detail.Logo,
                EditTime = detail.EditTime,
                OperId = detail.OperId
            };
            return response;
        }
    }
}
