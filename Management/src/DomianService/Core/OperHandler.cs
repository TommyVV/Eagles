using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Operator.Model;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Application.Model.Operator.Response;
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
        public ResponseBase EditOper(EditOperatorRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TbOper mod;
            var now = DateTime.Now;

            if (requset.Info.OperId > 0)
            {
                mod = new TbOper
                {
                    OperId=requset.Info.OperId,
                   // CreateTime=now,
                    GroupId=requset.Info.AuthorityGroupId,
                    OperName=requset.Info.AuthorityGroupName,
                    OrgId=requset.OrgId,
                    Password=requset.Info.Password,
                    Status=requset.Info.Status
                };

                int result = dataAccess.EditOper(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                mod = new TbOper
                {
                    OperId = requset.Info.OperId,
                    CreateTime = now,
                    GroupId = requset.Info.AuthorityGroupId,
                    OperName = requset.Info.AuthorityGroupName,
                    OrgId = requset.OrgId,
                    Password = requset.Info.Password,
                    Status = requset.Info.Status
                };

                int result = dataAccess.CreateOper(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        public ResponseBase RemoveOper(RemoveOperatorRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            //TODO 删除试卷 题目关系
            int result = dataAccess.RemoveOper(requset);

            if (result < 0)
            {
                response.IsSuccess = false;
            }

            return response;
            
        }

        public GetOperatorDetailResponse GetOperDetail(GetOperatorDetailRequset requset)
        {
            var response = new GetOperatorDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbOper detail = dataAccess.GetOperDetail(requset);

            if (detail == null) throw new Exception("无数据");

            response.Info = new OperatorDetail
            {
                Account = detail.OperName,
                Status = detail.Status,
                // Password=detail.Password,
                AuthorityGroupId = detail.GroupId,
                CreateTime = detail.CreateTime,
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
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbOper> list = dataAccess.GetOperList(requset) ?? new List<TbOper>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new Operator
            {
                CreateTime = x.CreateTime,
                OperId = x.OperId,
                OperName = x.OperName,
                AuthorityGroupName="",
            }).ToList();
            return response;
        }
    }
}
