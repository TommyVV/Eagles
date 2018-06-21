﻿using System.Collections.Generic;
using Eagles.Base;

namespace Eagles.Interface.Core.DataBase.ScrollAccess
{
    public interface IScrollAccess : IInterfaceBase
    {
        List<DomainService.Model.ScrollImage.TbScrollImage> GetScrollImg();

        List<DomainService.Model.News.TbSystemNews> GetScrollNews();
    }
}