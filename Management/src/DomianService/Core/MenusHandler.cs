using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Menus.Model;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Application.Model.Menus.Response;
using Eagles.Base;
using Eagles.DomainService.Model.App;
using Eagles.Interface.Core;
using Eagles.Interface.DataAccess;

namespace Eagles.DomainService.Core
{
    public class MenusHandler : IMenusHandler
    {

        private readonly IMenusDataAccess dataAccess;

        private readonly IOrganizationDataAccess OrgdataAccess;


        public MenusHandler(IMenusDataAccess dataAccess, IOrganizationDataAccess orgdataAccess)
        {
            this.dataAccess = dataAccess;
            OrgdataAccess = orgdataAccess;
        }
        public bool EditMenus(EditMenusRequset requset)
        {

            TbAppMenu mod;

            if (requset.Info.MenuId > 0)
            {
                mod = new TbAppMenu
                {
                    Level = requset.Info.MenuLevel,
                    MenuId = requset.Info.MenuId,
                    MenuName = requset.Info.MenuName,
                    OrgId = requset.Info.OrgId,
                    ParentMenuId = requset.Info.ParentId,
                    TargetUrl = requset.Info.MenuLink
                };
                int result = dataAccess.EditNews(mod);

                return result > 0;
            }
            else
            {
                mod = new TbAppMenu
                {
                    Level = requset.Info.MenuLevel,
                    //MenuId = requset.DetailInfo.MenuId,
                    MenuName = requset.Info.MenuName,
                    OrgId = requset.Info.OrgId,
                    ParentMenuId = requset.Info.ParentId,
                    TargetUrl = requset.Info.MenuLink
                };

                int result = dataAccess.CreateNews(mod);

                return result > 0;
            }
        }

        public bool RemoveMenus(RemoveMenusRequset requset)
        {

            int result = dataAccess.RemoveMenus(requset);

            return result > 0;
        }

        public GetMenusDetailResponse GetMenusDetail(GetMenusDetailRequest requset)
        {
            var response = new GetMenusDetailResponse();

            TbAppMenu detail = dataAccess.GetMenusDetail(requset);
            var orgdetail = OrgdataAccess.GetOrganizationDetail(new Application.Model.Organization.Requset.GetOrganizationDetailRequset
            {
                OrgId = detail.OrgId
            });

            if (detail == null) throw new TransactionException("M01", "无业务数据");

            response.Info = new Menus
            {
                MenuId = detail.MenuId,
                MenuLevel = detail.Level,
                MenuLink = detail.TargetUrl,
                OrgId = detail.OrgId,
                MenuName = detail.MenuName,
                ParentId = detail.ParentMenuId,
                OrgName = orgdetail?.OrgName
                // Category=detail.ViewCount
            };
            return response;
        }



        public GetMenusResponse GetMenus(GetMenusRequset requset)
        {
            var response = new GetMenusResponse
            {
                TotalCount = 0,
            };
            List<TbAppMenu> list = dataAccess.GetMenusList(requset, out int totalCount) ?? new List<TbAppMenu>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");

            var orginfo = OrgdataAccess.GetOrganizationList(list.Select(x => x.OrgId).ToList());

            response.TotalCount = totalCount;
            response.List = list.Select(x => new Menus
            {
                MenuId = x.MenuId,
                MenuLevel = x.Level,
                MenuLink = x.TargetUrl,
                OrgId = x.OrgId,
                MenuName = x.MenuName,
                ParentId = x.ParentMenuId,
                OrgName = orginfo.First(f => f.OrgId == x.OrgId).OrgName
            }).ToList();
            return response;
        }

        public GetSubordinateResponse GetSubordinate(GetSubordinateRequset requset)
        {
            var response = new GetSubordinateResponse
            {

            };

            List<TbAppMenu> list = dataAccess.GetSubordinate(requset) ?? new List<TbAppMenu>();

            if (list.Count == 0) throw new TransactionException("M01", "无业务数据");
            response.List = list.Select(x => new Menus
            {
                MenuId = x.MenuId,
                MenuLevel = x.Level,
                MenuLink = x.TargetUrl,
                OrgId = x.OrgId,
                MenuName = x.MenuName,
                ParentId = x.ParentMenuId,
            }).ToList();
            return response;
            
        }
    }
}
