using System.Collections.Generic;
using Eagles.Base;

namespace Eagles.Interface.Core.DataBase.ScrollAccess
{
    public interface IScrollAccess : IInterfaceBase
    {
        List<DomainService.Model.ScrollImage.ScrollImage> GetScrollImg();

        List<DomainService.Model.News.SystemNews> GetScrollNews();
    }
}