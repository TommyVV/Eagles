using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Operator.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Oper;

namespace Eagles.Interface.DataAccess
{
    public interface IOperDataAccess:IInterfaceBase
    {
        int EditOper(TbOper mod);
        int CreateOper(TbOper mod);
        int RemoveOper(RemoveOperatorRequset requset);
        TbOper GetOperDetail(GetOperatorDetailRequset requset);
        List<TbOper> GetOperList(GetOperatorRequset requset);
        int GetOperListByAuthorityGroupId(int requsetAuthorityGroupId);
    }
}
