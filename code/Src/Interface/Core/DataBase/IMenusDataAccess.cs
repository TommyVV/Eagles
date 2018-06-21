using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        int EditNews(TB_APP_MENU mod);

        int CreateNews(TB_APP_MENU mod);

        int RemoveMenus(RemoveMenusRequset requset);

        TB_APP_MENU GetMenusDetail(GetMenusDetailRequest requset);

        List<TB_APP_MENU> GetNewsList(GetMenusRequset requset);
    }
}
