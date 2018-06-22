using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Application.Model.Menus.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IMenusHandler : IInterfaceBase
    {
        /// <summary>
        /// 编辑 菜单
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase EditMenus(EditMenusRequset requset);

        /// <summary>
        /// 删除 菜单
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase RemoveMenus(RemoveMenusRequset requset);

        /// <summary>
        /// 菜单 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetMenusDetailResponse GetMenusDetail(GetMenusDetailRequest requset);

        /// <summary>
        /// 菜单 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetMenusResponse GetMenus(GetMenusRequset requset);
    }
}
