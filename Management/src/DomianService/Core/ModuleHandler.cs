using System;
using System.Collections.Generic;
using System.Linq;
using Eagles.Application.Model;
using Eagles.Application.Model.Column.Model;
using Eagles.Application.Model.Column.Requset;
using Eagles.Application.Model.Column.Response;
using Eagles.Application.Model.Enums;
using Eagles.DomainService.Model.App;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core
{
    public class ModuleHandler : IModuleHandler
    {
        private readonly IColumnDataAccess dataAccess;

        public ModuleHandler(IColumnDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public ResponseBase EditColumn(EditColumnRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

            TbAppModule mod;

            if (requset.Info.ColumnId > 0)
            {
                mod = new TbAppModule
                {

                    TragetUrl = requset.Info.ColumnAddress,
                    Priority = requset.Info.OrderBy,
                    SmallImageUrl = requset.Info.ColumnIcon,
                    ImageUrl = requset.Info.ColumnImg,
                    IndexDisplay = requset.Info.IsSetTop,
                    ModuleType = requset.Info.ModuleType,
                    OrgId = requset.Info.OrgId,
                    ModuleId = requset.Info.ColumnId,
                    ModuleName = requset.Info.ColumnName,
                    IndexPageCount = requset.Info.IndexPageCount
                };

                int result = dataAccess.EditColumn(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }
            else
            {
                mod = new TbAppModule
                {
                    TragetUrl = requset.Info.ColumnAddress,
                    Priority = requset.Info.OrderBy,
                    SmallImageUrl = requset.Info.ColumnIcon,
                    ImageUrl = requset.Info.ColumnImg,
                    IndexDisplay = requset.Info.IsSetTop,
                    ModuleType = requset.Info.ModuleType,
                    OrgId = requset.Info.OrgId,
                    ModuleName = requset.Info.ColumnName,
                    IndexPageCount = requset.Info.IndexPageCount
                };

                int result = dataAccess.CreateColumn(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;

        }

        public ResponseBase RemoveColumn(RemoveColumnRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            int result = dataAccess.RemoveColumn(requset);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public GetColumnDetailResponse GetColumnDetail(GetColumnDetailRequset requset)
        {
            var response = new GetColumnDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbAppModule detail = dataAccess.GetColumnDetail(requset);

            if (detail == null) throw new Exception("无数据");

            response.Info = new ColumnInfoDetails
            {
                //AuditStatus = AuditStatus.审核通过,
                ColumnAddress = detail.TragetUrl,
                ColumnId = detail.ModuleId,
                ColumnName = detail.ModuleName,
                OrderBy = detail.Priority,
                ColumnIcon=detail.SmallImageUrl,
                ColumnImg=detail.ImageUrl,
                IsSetTop=detail.IndexDisplay,
                ModuleType =detail.ModuleType,
            };
            return response;
        }

        public GetColumnResponse GetColumn(GetColumnRequset requset)
        {
            var response = new GetColumnResponse
            {
                TotalCount = 0,
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbAppModule> list = dataAccess.GetColumnList(requset) ?? new List<TbAppModule>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new ColumnInfo
            {
                AuditStatus = AuditStatus.审核通过,
                ColumnAddress = x.TragetUrl,
                ColumnId = x.ModuleId,
                ColumnName = x.ModuleName,
                OrderBy = x.Priority
            }).ToList();
            return response;
        }
    }
}
