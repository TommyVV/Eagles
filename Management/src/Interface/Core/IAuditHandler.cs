using Eagles.Application.Model;
using Eagles.Application.Model.Audit.Requset;
using Eagles.Application.Model.Audit.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IAuditHandler : IInterfaceBase
    {
        /// <summary>
        /// 添加审核流水
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        ResponseBase CreateAudit(CreateAuditRequset requset);

        /// <summary>
        /// 查询审核流水列表 列表
        /// </summary>
        /// <param name="requset"></param>
        /// <returns></returns>
        GetAuditResponse GetAuditList(GetAuditRequest requset);
    }
}
