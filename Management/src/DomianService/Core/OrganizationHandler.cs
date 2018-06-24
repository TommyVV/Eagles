using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Organization.Model;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.Organization.Response;
using Eagles.DomainService.Model.Org;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;
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

        public ResponseBase EditOrganization(EditOrganizationRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

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
                };

                int result = dataAccess.EditOrganization(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
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
                int result = dataAccess.CreateOrganization(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        public ResponseBase RemoveOrganization(RemoveOrganizationRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            int result = dataAccess.RemoveOrganization(requset);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public GetOrganizationResponse Organization(GetOrganizationRequset requset)
        {
            var response = new GetOrganizationResponse
            {
                TotalCount = 0,
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbOrgInfo> list = dataAccess.GetOrganizationList(requset) ?? new List<TbOrgInfo>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new Organization
            {
                Address = x.Address,
                City = x.City,
                CreateTime = x.CreateTime,
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
                ErrorCode = "00",
                Message = "成功",
            };
            TbOrgInfo detail = dataAccess.GetOrganizationDetail(requset);

            if (detail == null) throw new Exception("无数据");

            response.Info = new OrganizationDetail
            {
                Address = detail.Address,
                City = detail.City,
                CreateTime = detail.CreateTime,
                District = detail.District,
                OrgId = detail.OrgId,
                OrgName = detail.OrgName,
                Province = detail.Province,
            };
            return response;
        }
    }
}
