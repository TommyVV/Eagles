using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.SystemMessage.Requset;
using Eagles.Application.Model.SystemMessage.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public  interface ISystemNewsHandler : IInterfaceBase
    {

        bool EditSystemNews(EditSystemNewsRequset requset);

        bool RemoveSystemNews(RemoveSystemNewsRequset requset);

        GetSystemNewsResponse SystemNews(GetSystemNewsRequset requset);

        GetSystemNewsDetailResponse GetSystemNewsDetail(GetSystemNewsDetailRequset requset);
    }
}
