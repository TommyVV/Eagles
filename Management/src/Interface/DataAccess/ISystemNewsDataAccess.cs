using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.SystemMessage.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.News;

namespace Eagles.Interface.DataAccess
{
    public interface ISystemNewsDataAccess : IInterfaceBase
    {
        int EditSystemNews(TbSystemNews mod);
        int CreateSystemNews(TbSystemNews mod);
        int RemoveSystemNews(RemoveSystemNewsRequset requset);
        List<TbSystemNews> SystemNews(GetSystemNewsRequset requset,out int  totalCount);
        TbSystemNews GetSystemNewsDetail(GetSystemNewsDetailRequset requset);
    }
}
