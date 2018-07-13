using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Login.Model;
using Eagles.Application.Model.Login.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Oper;
using Eagles.DomainService.Model.User;

namespace Eagles.Interface.DataAccess
{
    public interface ILoginDataAccess : IInterfaceBase
    {
       
        void UpdateOperErrorCount(TbOper operId);
        void CreateverificationInfo(Verification verification);
        int GetverificationInfo(Verification verification);
        void InsertToken(TbUserToken tbUserToken);
        TbUserToken GetUserToken(string token, int tokenType);
    }
}
