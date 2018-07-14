using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Audit.Model;
using Eagles.Application.Model.Audit.Requset;
using Eagles.Application.Model.Audit.Response;
using Eagles.Application.Model.Menus.Model;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Application.Model.Menus.Response;
using Eagles.Base;
using Eagles.DomainService.Model.App;
using Eagles.DomainService.Model.Audit;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class AuditHandler : IAuditHandler
    {

        private readonly IAuditDataAccess dataAccess;

        public AuditHandler(IAuditDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool CreateAudit(CreateAuditRequset requset)
        {


            var now = DateTime.Now;
            var mod = new TbReview
            {
                BranchId = requset.BranchId,
                CreateTime = now,
                NewsId = requset.Info.Id,
                NewsType = requset.Info.NewsType,
                OperId = requset.Info.UserId,
                OrgId = requset.OrgId,
                Result = "",
                ReviewId = requset.Info.AuditId,
                ReviewStatus = requset.Info.AuditStatus,

            };

            return dataAccess.CreateAudit(mod) > 0;


        }

        public GetAuditResponse GetAuditList(GetAuditRequest requset)
        {
            var response = new GetAuditResponse
            {
                TotalCount = 0,
               
            };
            List<TbReview> list = dataAccess.GetAuditList(requset);

            if (list.Count == 0) throw new TransactionException("M01","无业务数据");

            response.List = list.Select(x => new Audit
            {
                AuditId = x.ReviewId,
                // AuditName=x.
                AuditStatus = x.ReviewStatus,
                CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
                Id = x.NewsId,
                NewsType = x.NewsType,
                UserId = x.OperId,
            }).ToList();
            return response;
        }
    }
}
