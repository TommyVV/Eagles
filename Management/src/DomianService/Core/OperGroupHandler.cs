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
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.Authority;
using Eagles.DomainService.Model.Oper;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class OperGroupHandler : IOperGroupHandler
    {

        private readonly IOperGroupDataAccess dataAccess;

        private readonly IOperDataAccess OperdataAccess;

        private readonly ICacheHelper cacheHelper;

        public OperGroupHandler(IOperGroupDataAccess dataAccess, IOperDataAccess operdataAccess, ICacheHelper cacheHelper)
        {
            this.dataAccess = dataAccess;
            OperdataAccess = operdataAccess;
            this.cacheHelper = cacheHelper;
        }

        public bool EditOperGroup(EditAuthorityGroupRequset requset)
        {


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

                return dataAccess.EditOperGroup(mod) > 0;

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

                return dataAccess.CreateOperGroup(mod) > 0;

            }

        }

        public bool RemoveOperGroup(RemoveAuthorityGroupInfoRequset requset)
        {

            var result = OperdataAccess.GetOperListByAuthorityGroupId(requset.AuthorityGroupId);
            if (result > 0)
            {
                throw new TransactionException("M02", "请先移除该群众下用户在进行操作");
            }

            return dataAccess.RemoveOperGroup(requset) > 0;

        }

        public GetAuthorityGroupDetailResponse GetOperGroupDetail(GetAuthorityGroupDetailRequset requset)
        {
            var response = new GetAuthorityGroupDetailResponse
            {
            };
            TbOperGroup detail = dataAccess.GetOperGroupDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new AuthorityGroup
            {
                AuthorityGroupId = detail.GroupId,
                AuthorityGroupName = detail.GroupName,
                OrgId = detail.OrgId,
                CreateTime = detail.CreateTime.ToString("yyyy-MM-dd"),
            };
            return response;
        }

        public GetAuthorityGroupResponse GetOperGroupList(GetAuthorityGroupRequset requset)
        {

            var response = new GetAuthorityGroupResponse
            {
                TotalCount = 0,
            };
            List<TbOperGroup> list = dataAccess.GetOperGroupList(requset) ?? new List<TbOperGroup>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.List = list.Select(x => new AuthorityGroup
            {
                AuthorityGroupId = x.GroupId,
                AuthorityGroupName = x.GroupName,
                OrgId = x.OrgId,
                CreateTime = x.CreateTime.ToString("yyyy-MM-dd"),
            }).ToList();
            return response;
        }

        public bool EditAuthorityGroupSetUp(EditAuthorityGroupSetUpRequset requset)
        {

            if (!(requset.AuthorityInfo.Count > 0 && requset.GroupId > 0))
            {
                throw new TransactionException("M03", "数据异常");
            }

            int result = dataAccess.RemoveAuthorityGroupSetUp(requset.GroupId);

            if (result > 0)
            {
                var now = DateTime.Now;
                List<TbAuthority> list = requset.AuthorityInfo.Select(x => new TbAuthority
                {
                    CreateTime = now,
                    FunCode = x.FunCode,
                    OperId = 0,
                    GroupId = requset.GroupId,
                    OrgId = requset.OrgId,
                    EditTime = now,
                }).ToList();

                return dataAccess.CreateAuthorityGroupSetUp(list) > 0;

            }

            throw new TransactionException("M04", "权限组维护失败");

        }

        public GetAuthorityGroupSetUpResponse GetAuthorityGroupSetUp(GetAuthorityGroupSetUpRequest requset)
        {
            var response = new GetAuthorityGroupSetUpResponse
            {
            };

           
            List<TbAuthority> list = dataAccess.GetAuthorityGroupSetUp(requset.GroupId) ?? new List<TbAuthority>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            response.List = list.Select(x => new AuthorityInfo
            {
                FunCode = x.FunCode,
                CreateTime = x.CreateTime,
            }).ToList();
            return response;
        }

        public GetAuthorityGroupSetUpResponse GetAuthorityByToken(RequestBase request)
        {
            //get user group 
            var userInfo = cacheHelper.GetData<TbUserToken>(request.Token);
            var operInfo = OperdataAccess.GetOperDetail(userInfo.UserId);
            if (operInfo == null)
            {
                throw new TransactionException("M01", "无业务数据");

            }
            var list = dataAccess.GetAuthorityGroupSetUp(operInfo.GroupId) ?? new List<TbAuthority>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");
            var response = new GetAuthorityGroupSetUpResponse
            {
                List = list.Select(x => new AuthorityInfo
                {
                    FunCode = x.FunCode,
                    CreateTime = x.CreateTime,
                }).ToList()
            };
            return response;
        }
    }
}
