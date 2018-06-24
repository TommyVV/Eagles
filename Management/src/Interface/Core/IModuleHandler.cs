using Eagles.Application.Model;
using Eagles.Application.Model.Column.Requset;
using Eagles.Application.Model.Column.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
     public interface IModuleHandler : IInterfaceBase
    {

        /// <summary>
        /// 编辑 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase EditColumn(EditColumnRequset requset);

        /// <summary>
        /// 删除 栏目
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase RemoveColumn(RemoveColumnRequset requset);

        /// <summary>
        /// 栏目 详情
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetColumnDetailResponse GetColumnDetail(GetColumnDetailRequset requset);

        /// <summary>
        /// 栏目 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetColumnResponse GetColumn(GetColumnRequset requset);
    }
}
