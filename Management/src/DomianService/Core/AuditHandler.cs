using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eagles.Application.Model;
using Eagles.Application.Model.Audit.Model;
using Eagles.Application.Model.Audit.Requset;
using Eagles.Application.Model.Audit.Response;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.Audit;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class AuditHandler : IAuditHandler
    {

        private readonly IAuditDataAccess dataAccess;

        private readonly ICacheHelper cacheHelper;

        private readonly IOperGroupHandler OperGroupdataAccess;

        public AuditHandler(IAuditDataAccess dataAccess, ICacheHelper cacheHelper, IOperGroupHandler operGroupdataAccess)
        {
            this.dataAccess = dataAccess;
            this.cacheHelper = cacheHelper;
            OperGroupdataAccess = operGroupdataAccess;
        }

        public bool CreateAudit(CreateAuditRequset requset)
        {


          



            var token = cacheHelper.GetData<TbUserToken>(requset.Token);
            var now = DateTime.Now;
            var mod = new TbReview
            {
                BranchId = token.BranchId,
                CreateTime = now,
                NewsId = requset.Info.Id,
                NewsType = requset.Info.NewsType,
                OperId = requset.Info.UserId,
                OrgId = token.OrgId,
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

        public bool Audit(AuditInfo requset)
        {

            var authority = OperGroupdataAccess.GetAuthorityByToken(new RequestBase() {Token = requset.Token});


            var sql = new StringBuilder();

            sql.AppendFormat(@"UPDATE `eagles`.`{0}`
SET
`Status` = 1
WHERE `{1}` = @Id;
");

            switch (requset.AuditType)
            {
                case AuditType.产品:
                    if (authority.List.Any(x => x.FunCode == "ProductAudit"))
                    {
                        // AuditTableName = "tb_product";
                        sql.AppendFormat("{0},{1}", "tb_product", "ProdId");
                    }

                    break;
                case AuditType.任务:
                    if (authority.List.Any(x => x.FunCode == "TaskAudit"))
                    {
                        sql.AppendFormat("{0},{1}", "tb_task", "TaskId");
                    }

                    break;
                case AuditType.新闻:
                    if (authority.List.Any(x => x.FunCode == "NewsAudit"))
                    {
                        sql.AppendFormat("{0},{1}", "tb_news", "NewsId");

                    }

                    break;
                case AuditType.活动:
                    if (authority.List.Any(x => x.FunCode == "ActivityAudit"))
                    {
                        sql.AppendFormat("{0},{1}", "tb_activity", "ActivityId");
                    }

                    break;
                case AuditType.用户:
                    if (authority.List.Any(x => x.FunCode == "UserAudit"))
                    {
                        sql.AppendFormat("{0},{1}", "tb_user_info", "UserId");
                    }

                    break;
                default:
                    throw new TransactionException("", "无该审核类型");
            }

            return dataAccess.Audit(sql.ToString(), requset.AuditId) > 0;



        }
    }
}
