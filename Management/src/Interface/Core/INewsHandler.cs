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
        bool EditNews(EditNewRequset requset);

        /// <summary>
        /// 删除 新闻
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        bool RemoveNews(RemoveNewRequset requset);

        /// <summary>
        /// 新闻 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetNewDetailResponse GetNewsDetail(GetNewDetailRequset requset);

        /// <summary>
        /// 新闻 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetNewResponse GetNews(GetNewRequset requset);
    }
}
