using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Column.Requset;
using Eagles.DomainService.Model.Column;
using Eagles.Interface.Core.DataBase;

namespace Eagles.DomainService.Core.DataBase
{
    public class ColumnDataAccess: IColumnDataAccess
    {
        public List<TB_APP_MODULE> GetColumnList(GetColumnRequset requset)
        {
            throw new NotImplementedException();
        }
    }
}
