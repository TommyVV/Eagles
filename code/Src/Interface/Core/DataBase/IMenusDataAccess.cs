using System.Collections.Generic;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.App;

namespace Eagles.Interface.Core.DataBase
{
    public interface IMenusDataAccess : IInterfaceBase
    {

        //int EditMenus(TB_PRODUCT mod);
        //int CreateMenus(TB_PRODUCT mod);
        //int RemoveMenus(RemoveMenusRequset requset);
        //TB_PRODUCT GetMenusDetail(GetMenusDetailRequset requset);
        //List<TB_PRODUCT> GetNewsList(GetMenusRequest requset);
        int EditNews(TbAppMenu mod);

        int CreateNews(TbAppMenu mod);

        int RemoveMenus(RemoveMenusRequset requset);

        TbAppMenu GetMenusDetail(GetMenusDetailRequest requset);

        List<TbAppMenu> GetNewsList(GetMenusRequset requset);
    }
}