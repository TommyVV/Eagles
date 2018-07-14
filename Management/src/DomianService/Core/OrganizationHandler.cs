using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Application.Model.Organization.Model;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.Organization.Response;
using Eagles.Base;
using Eagles.DomainService.Model.Org;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
   public  class OrganizationHandler : IOrganizationHandler
    {
        private readonly IOrganizationDataAccess dataAccess;


        public OrganizationHandler(IOrganizationDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool EditOrganization(EditOrganizationRequset requset)
        {


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
                    //     CreateTime= now,
                    District = requset.Info.District,
                    EditTime = now,
                    Logo = requset.Info.Logo,
                    OperId = 0,
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
                    OperId = 0,
                };
                return dataAccess.CreateOrganization(mod) > 0;


            }

        }

        public bool RemoveOrganization(RemoveOrganizationRequset requset)
        {
            
       
            return dataAccess.RemoveOrganization(requset)>0;

           
        }

        public GetOrganizationResponse Organization(GetOrganizationRequset requset)
        {
            var response = new GetOrganizationResponse
            {
                TotalCount = 0,
                
            };
            List<TbOrgInfo> list = dataAccess.GetOrganizationList(requset) ?? new List<TbOrgInfo>();

            if (list.Count == 0) throw new TransactionException("M01","无业务数据");

            response.List = list.Select(x => new Organization
            {
                Address = x.Address,
                City = x.City,
                CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
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

            if (detail == null) throw new TransactionException("M01","无业务数据");

            response.Info = new OrganizationDetail
            {
                Address = detail.Address,
                City = detail.City,
                CreateTime = detail.CreateTime.ToString("yyyy-MM-dd"),
                District = detail.District,
                OrgId = detail.OrgId,
                OrgName = detail.OrgName,
                Province = detail.Province,
            };
            return response;
        }
    }
}
