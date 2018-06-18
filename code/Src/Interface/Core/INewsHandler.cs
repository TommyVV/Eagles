using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.News.Requset;
using Eagles.Application.Model.News.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface INewsHandler : IInterfaceBase
    {
        /// <summary>
        /// 编辑 新闻
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase EditNews(EditNewsRequset requset);

        /// <summary>
        /// 删除 新闻
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase RemoveNews(RemoveNewsRequset requset);

        /// <summary>
        /// 新闻 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetNewsDetailResponse GetNewsDetail(GetNewsDetailRequset requset);

        /// <summary>
        /// 新闻 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetNewsResponse GetNews(GetNewsRequset requset);
    }
}
