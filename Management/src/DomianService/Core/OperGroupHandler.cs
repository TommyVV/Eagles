using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.AuthorityGroup.Model;
using Eagles.Application.Model.AuthorityGroup.Requset;
using Eagles.Application.Model.AuthorityGroup.Response;
using Eagles.Application.Model.AuthorityGroupSetUp.Model;
using Eagles.Application.Model.AuthorityGroupSetUp.Requset;
using Eagles.Application.Model.AuthorityGroupSetUp.Response;
using Eagles.DomainService.Model.Authority;
using Eagles.DomainService.Model.Oper;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class OperGroupHandler : IOperGroupHandler
    {

        private readonly IOperGroupDataAccess dataAccess;
        private readonly IOperDataAccess OperdataAccess;

        public OperGroupHandler(IOperGroupDataAccess dataAccess, IOperDataAccess operdataAccess)
        {
            this.dataAccess = dataAccess;
            OperdataAccess = operdataAccess;
        }
        public ResponseBase EditOperGroup(EditAuthorityGroupRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TbOperGroup mod;
            var now = DateTime.Now;

            if (requset.Info.AuthorityGroupId > 0)
            {
                mod = new TbOperGroup
                {
                    // CreateTime=requset.Info.CreateTime,
                    EditTime = now,
                    GroupId = requset.Info.AuthorityGroupId,
                    GroupName = requset.Info.AuthorityGroupName,
                    OrgId = requset.Info.OrgId
                };

                int result = dataAccess.EditOperGroup(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                mod = new TbOperGroup
                {
                    EditTime = now,
                    CreateTime = now,
                    GroupId = requset.Info.AuthorityGroupId,
                    GroupName = requset.Info.AuthorityGroupName,
                    OrgId = requset.Info.OrgId
                };

                int result = dataAccess.CreateOperGroup(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        public ResponseBase RemoveOperGroup(RemoveAuthorityGroupInfoRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            var result1 = OperdataAccess.GetOperListByAuthorityGroupId(requset.AuthorityGroupId);
            if (result1 > 0)
            {
                throw new Exception("有用户属于改群组");
            }

            var result = dataAccess.RemoveOperGroup(requset);

            if (result < 0)
            {
                response.IsSuccess = false;
            }

            return response;
        }

        public GetAuthorityGroupDetailResponse GetOperGroupDetail(GetAuthorityGroupDetailRequset requset)
        {
            var response = new GetAuthorityGroupDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbOperGroup detail = dataAccess.GetOperGroupDetail(requset);

            if (detail == null) throw new Exception("无数据");

            response.Info = new AuthorityGroup
            {
                AuthorityGroupId = detail.GroupId,
                AuthorityGroupName = detail.GroupName,
                OrgId = detail.OrgId,
                CreateTime = detail.CreateTime,
            };
            return response;
        }

        public GetAuthorityGroupResponse GetOperGroupList(GetAuthorityGroupRequset requset)
        {

            var response = new GetAuthorityGroupResponse
            {
                TotalCount = 0,
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbOperGroup> list = dataAccess.GetOperGroupList(requset) ?? new List<TbOperGroup>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new AuthorityGroup
            {
                AuthorityGroupId = x.GroupId,
                AuthorityGroupName = x.GroupName,
                OrgId = x.OrgId,
                CreateTime = x.CreateTime,
            }).ToList();
            return response;
        }

        public ResponseBase EditAuthorityGroupSetUp(EditAuthorityGroupSetUpRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            if (!(requset.AuthorityInfo.Count > 0 && requset.GroupId > 0))
            {
                throw new Exception("数据异常");
            }

            int result = dataAccess.RemoveAuthorityGroupSetUp(requset.GroupId);

            if (result > 0)
            {
                var now = DateTime.Now;
                List<TbAuthority> list = requset.AuthorityInfo.Select(x => new TbAuthority
                {
                    CreateTime=now,
                    FunCode=x.FunCode,
                    OperId=0,
                    GroupId=requset.GroupId,
                    OrgId=requset.OrgId,
                    EditTime=now,
                }).ToList();

                int result1 = dataAccess.CreateAuthorityGroupSetUp(list);
                if (result1 < 0)
                {
                    response.IsSuccess = false;
                }
            }
            else
            {
                response.IsSuccess = false;
            }

            return response;

        }

        public GetAuthorityGroupSetUpResponse GetAuthorityGroupSetUp(GetAuthorityGroupSetUpRequest requset)
        {
            var response = new GetAuthorityGroupSetUpResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbAuthority> list = dataAccess.GetAuthorityGroupSetUp(requset) ?? new List<TbAuthority>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new AuthorityInfo
            {
                FunCode = x.FunCode,             
                CreateTime = x.CreateTime,
            }).ToList();
            return response;
        }
    }
}
