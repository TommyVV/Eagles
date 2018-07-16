using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model.Column.Model;
using Eagles.Application.Model.Column.Requset;
using Eagles.Application.Model.Column.Response;
using Eagles.Application.Model.Enums;
using Eagles.Base;
using Eagles.Base.Cache;
using Eagles.DomainService.Model.App;
using Eagles.DomainService.Model.User;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class ModuleHandler : IModuleHandler
    {
        private readonly IColumnDataAccess dataAccess;

        private readonly ICacheHelper cacheHelper;

        public ModuleHandler(IColumnDataAccess dataAccess, ICacheHelper cacheHelper)
        {
            this.dataAccess = dataAccess;
            this.cacheHelper = cacheHelper;
        }

        public bool EditColumn(EditColumnRequset requset)
        {

            TbAppModule mod;
            var userToken = cacheHelper.GetData<TbUserToken>(requset.Token);
            if (requset.Info.ColumnId > 0)
            {
                mod = new TbAppModule
                {

                    TargetUrl = requset.Info.TargetUrl,
                    Priority = requset.Info.OrderBy,
                    SmallImageUrl = requset.Info.ColumnIcon,
                    ImageUrl = requset.Info.ColumnImg,
                    IndexDisplay = requset.Info.IsSetTop,
                    ModuleType = requset.Info.ModuleType,
                    OrgId = userToken.OrgId,
                    ModuleId = requset.Info.ColumnId,
                    ModuleName = requset.Info.ColumnName,
                   // IndexPageCount = requset.Info.IndexPageCount
                };

                return dataAccess.EditColumn(mod) > 0;

            }
            else
            {
                mod = new TbAppModule
                {
                    TargetUrl = requset.Info.TargetUrl,
                    Priority = requset.Info.OrderBy,
                    SmallImageUrl = requset.Info.ColumnIcon,
                    ImageUrl = requset.Info.ColumnImg,
                    IndexDisplay = requset.Info.IsSetTop,
                    ModuleType = requset.Info.ModuleType,
                    OrgId = userToken.OrgId,
                    ModuleName = requset.Info.ColumnName,
                   // IndexPageCount = requset.Info.IndexPageCount
                };

                return dataAccess.CreateColumn(mod) > 0;
            }

        }

        public bool RemoveColumn(RemoveColumnRequset requset)
        {
            return dataAccess.RemoveColumn(requset) > 0;
        }

        public GetColumnDetailResponse GetColumnDetail(GetColumnDetailRequset requset)
        {
            var response = new GetColumnDetailResponse
            {
            };
            TbAppModule detail = dataAccess.GetColumnDetail(requset);

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new ColumnInfoDetails
            {
                //AuditStatus = AuditStatus.审核通过,
                TargetUrl = detail.TargetUrl,
                ColumnId = detail.ModuleId,
                ColumnName = detail.ModuleName,
                OrderBy = detail.Priority,
                ColumnIcon = detail.SmallImageUrl,
                ColumnImg = detail.ImageUrl,
                IsSetTop = detail.IndexDisplay,
                ModuleType = detail.ModuleType,
              //  IndexPageCount = detail.IndexPageCount
                AuditStatus = AuditStatus.审核通过 //todo 
            };
            return response;
        }

        public GetColumnResponse GetColumn(GetColumnRequset requset)
        {
            var response = new GetColumnResponse
            {
                TotalCount = 0,
            };
            var tokenInfo = cacheHelper.GetData<TbUserToken>(requset.Token);
            var list = dataAccess.GetColumnList(requset, out int totalCount, tokenInfo.OrgId) ?? new List<TbAppModule>();

            if (list.Count == 0) throw new TransactionException("01", "无业务数据");
            response.TotalCount = totalCount;
            response.List = list.Select(x => new ColumnInfo
            {
                AuditStatus = AuditStatus.审核通过,
                TargetUrl = x.TargetUrl,
                ColumnId = x.ModuleId,
                ColumnName = x.ModuleName,
                OrderBy = x.Priority
            }).ToList();
            return response;
        }
    }
}
