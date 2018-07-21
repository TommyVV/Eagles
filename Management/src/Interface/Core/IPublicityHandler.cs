using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Publicity.Request;
using Eagles.Application.Model.Publicity.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IPublicityHandler: IInterfaceBase
    {
        GetPublicActivityDetailResponse GetPublicActivityDetail(GetPublicActivityDetailRequest requset);
        GetPublicActivityResponse GetPublicActivity(RequestBase requset);
        GetPublicTaskDetailResponse GetPublicTaskDetail(GetPublicTaskDetailRequest requset);
        GetPublicTaskResponse GetPublicTask(RequestBase requset);
        GetAritcleDetailResponse GetAritcleDetail(GetPublicArticleDetailRequest requset);
        GetPublicAritcleResponse GetPublicArticle(RequestBase requset);
    }
}
