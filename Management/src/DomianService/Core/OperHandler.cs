using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Operator.Model;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Application.Model.Operator.Response;
using Eagles.Base;
using Eagles.DomainService.Model.News;
using Eagles.DomainService.Model.Oper;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class OperHandler: IOperHandler
    {
        private readonly IOperDataAccess dataAccess;

        public OperHandler(IOperDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public bool EditOper(EditOperatorRequset requset)
        {


            TbOper mod;
            var now = DateTime.Now;

            if (requset.Info.OperId > 0)
            {
                mod = new TbOper
                {
                    OperId=requset.Info.OperId,
                   // CreateTime=now,
                    GroupId=requset.Info.AuthorityGroupId,
                    OperName =requset.Info.OperName,
                    OrgId=requset.OrgId,
                    Password=requset.Info.Password,
                    Status=requset.Info.Status
                };

                return dataAccess.EditOper(mod)>0;

               
            }
            else
            {
                mod = new TbOper
                {
                    OperId = requset.Info.OperId,
                    CreateTime = now,
                    GroupId = requset.Info.AuthorityGroupId,
                    OperName = requset.Info.OperName,
                    OrgId = requset.OrgId,
                    Password = requset.Info.Password,
                    Status = requset.Info.Status
                };

                return dataAccess.CreateOper(mod)>0;
                
            }
            
        }

        public bool RemoveOper(RemoveOperatorRequset requset)
        {
           
            return dataAccess.RemoveOper(requset)>0;

        }

        public GetOperatorDetailResponse GetOperDetail(GetOperatorDetailRequset requset)
        {
            var response = new GetOperatorDetailResponse();
            TbOper detail = dataAccess.GetOperDetail(Convert.ToInt32(requset.OperId));

            if (detail == null) throw new TransactionException("M01","无业务数据");

            response.Info = new OperatorDetail
            {
                //Account = detail.OperName,
                Status = detail.Status,
                 Password=detail.Password,
                AuthorityGroupId = detail.GroupId,
                CreateTime = detail.CreateTime.ToString("yyyy-MM-dd"),
                OperId = detail.OperId,
                OperName = detail.OperName,
                
            };
            return response;
        }

        public GetOperatorResponse GetOperList(GetOperatorRequset requset)
        {

            var response = new GetOperatorResponse
            {
                TotalCount = 0,
            };
            List<TbOper> list = dataAccess.GetOperList(requset,out int totalCount) ?? new List<TbOper>();

            if (list.Count == 0) throw new TransactionException("M01","无业务数据");

            response.TotalCount = totalCount;
            response.List = list.Select(x => new Operator
            {
                CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
                OperId = x.OperId,
                OperName = x.OperName,
               // AuthorityGroupName="",
            }).ToList();
            return response;
        }
    }
}
