using System.Collections.Generic;
using Eagles.Application.Model.Audit.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Audit;

namespace Eagles.Interface.DataAccess
{
    public interface IAuditDataAccess : IInterfaceBase
    {
        int CreateAudit(TbReview mod);
        List<TbReview> GetAuditList(GetAuditRequest requset);
        int Audit(string sql,int auditId);
    }
}