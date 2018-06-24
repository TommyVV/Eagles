﻿using System.Collections.Generic;
using Eagles.Application.Model.Menus.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.App;

namespace Eagles.Interface.DataAccess
{
    public interface IMenusDataAccess : IInterfaceBase
    {

      
        int EditNews(TbAppMenu mod);

        int CreateNews(TbAppMenu mod);

        int RemoveMenus(RemoveMenusRequset requset);

        TbAppMenu GetMenusDetail(GetMenusDetailRequest requset);

        List<TbAppMenu> GetNewsList(GetMenusRequset requset);
    }
}