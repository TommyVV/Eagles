using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Column.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Column;

namespace Eagles.Interface.Core.DataBase
{
    public interface INewsDataAccess : IInterfaceBase
    {
        List<TB_APP_MODULE> GetColumnList(GetColumnRequset requset);
        TB_APP_MODULE GetColumnDetail(GetColumnDetailRequset requset);
        int RemoveColumn(RemoveColumnRequset requset);
        int EditColumn(TB_APP_MODULE mod);
        int CreateColumn(TB_APP_MODULE mod);
    }
}
