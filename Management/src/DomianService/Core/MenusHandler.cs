using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Menus.Model;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Application.Model.Menus.Response;
using Eagles.DomainService.Model.App;
using Eagles.Interface.Core;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core
{
   public  class MenusHandler: IMenusHandler
    {

        private readonly IMenusDataAccess dataAccess;

        public MenusHandler(IMenusDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        public ResponseBase EditMenus(EditMenusRequset requset)
        {

            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };

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
                    TragetUrl = requset.Info.MenuLink
                };
                int result = dataAccess.EditNews(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
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
                    TragetUrl = requset.Info.MenuLink
                };

                int result = dataAccess.CreateNews(mod);

                if (result > 0)
                {
                    response.IsSuccess = true;
                }
            }

            return response;
        }

        public ResponseBase RemoveMenus(RemoveMenusRequset requset)
        {
            var response = new ResponseBase
            {
                ErrorCode = "00",
                Message = "成功",
            };
            int result = dataAccess.RemoveMenus(requset);

            if (result > 0)
            {
                response.IsSuccess = true;
            }

            return response;
        }

        public GetMenusDetailResponse GetMenusDetail(GetMenusDetailRequest requset)
        {
            var response = new GetMenusDetailResponse
            {
                ErrorCode = "00",
                Message = "成功",
            };
            TbAppMenu detail = dataAccess.GetMenusDetail(requset);


            if (detail == null) throw new Exception("无数据");

            response.Info = new Menus
            {
                MenuId = detail.MenuId,
                MenuLevel=detail.Level,
                MenuLink=detail.TragetUrl,
                OrgId=detail.OrgId,
                MenuName=detail.MenuName,
                ParentId=detail.ParentMenuId
                // Category=detail.ViewCount
            };
            return response;
        }

        public GetMenusResponse GetMenus(GetMenusRequset requset)
        {
            var response = new GetMenusResponse
            {
                TotalCount = 0,
                ErrorCode = "00",
                Message = "成功",
            };
            List<TbAppMenu> list = dataAccess.GetNewsList(requset) ?? new List<TbAppMenu>();

            if (list.Count == 0) throw new Exception("无数据");

            response.List = list.Select(x => new Menus
            {
                MenuId = x.MenuId,
                MenuLevel = x.Level,
                MenuLink = x.TragetUrl,
                OrgId = x.OrgId,
                MenuName = x.MenuName,
                ParentId = x.ParentMenuId
            }).ToList();
            return response;
        }
    }
}
