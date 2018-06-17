using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Column.Requset;
using Eagles.DomainService.Model.Column;

namespace Eagles.Interface.Core.DataBase
{
    public interface IColumnDataAccess
    {
        List<TB_APP_MODULE> GetColumnList(GetColumnRequset requset);
        TB_APP_MODULE GetColumnDetail(GetColumnDetailRequset requset);
        int RemoveColumn(RemoveColumnRequset requset);
        int EditColumn(TB_APP_MODULE mod);
    }
}
