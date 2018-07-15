using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Application.Model.Branch.Model;
using Eagles.Application.Model.Branch.Requset;
using Eagles.Application.Model.Branch.Response;
using Eagles.Base;
using Eagles.DomainService.Model.Org;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class BranchHandler : IBranchHandler
    {
        private readonly IBranchDataAccess dataAccess;

        private readonly IOrganizationDataAccess OrganizationdataAccess;
        public BranchHandler(IBranchDataAccess dataAccess, IOrganizationDataAccess organizationdataAccess)
        {
            this.dataAccess = dataAccess;
            OrganizationdataAccess = organizationdataAccess;
        }

        public bool EditBranch(EditBranchRequset requset)
        {


            TbBranch mod;
            var now = DateTime.Now;
            if (requset.Info.BranchId > 0)
            {
                mod = new TbBranch
                {
                    BranchDesc = requset.Info.BranchDesc,
                    BranchId = requset.Info.BranchId,
                    BranchName = requset.Info.BranchName,
                    CreateTime = now,
                    OrgId = requset.Info.OrgId,

                };

                return dataAccess.EditBranch(mod) > 0;


            }
            else
            {
                mod = new TbBranch
                {
                    BranchDesc = requset.Info.BranchDesc,
                    BranchId = requset.Info.BranchId,
                    BranchName = requset.Info.BranchName,
                    CreateTime = now,
                    OrgId = requset.Info.OrgId
                };
                return dataAccess.CreateBranch(mod) > 0;


            }

        }

        public bool RemoveBranch(RemoveBranchRequset requset)
        {


            return dataAccess.RemoveBranch(requset) > 0;


        }

        public GetBranchResponse Branch(GetBranchRequset requset)
        {
            var response = new GetBranchResponse
            {
                TotalCount = 0,

            };
            List<TbBranch> list = dataAccess.GetBranchList(requset,out int totalcount) ?? new List<TbBranch>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");


            var orgInfo = OrganizationdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());
            response.TotalCount = totalcount;
            response.List = list.Select(x => new Branch
            {
                BranchName = x.BranchName,
                BranchId = x.BranchId,
                OrgId = x.OrgId,
                OrgName = orgInfo.First(o => o.OrgId == x.OrgId).OrgName
            }).ToList();
            return response;
        }

        public GetBranchDetailResponse GetBranchDetail(GetBranchDetailRequset requset)
        {
            var response = new GetBranchDetailResponse
            {

            };
            TbBranch detail = dataAccess.GetBranchDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            var orginfo = OrganizationdataAccess.GetOrganizationDetail(
                new Application.Model.Organization.Requset.GetOrganizationDetailRequset
                {
                    OrgId = detail.OrgId
                });

            response.Info = new BranchDetail
            {
                BranchDesc = detail.BranchDesc,
                BranchId = detail.BranchId,
                BranchName = detail.BranchName,
                CreateTime = detail.CreateTime,
                OrgId = detail.OrgId,
                OrgName = orginfo.OrgName
            };
            return response;
        }
    }
}
