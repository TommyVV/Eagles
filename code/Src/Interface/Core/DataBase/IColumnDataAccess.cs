using System;
using System.Collections.Generic;
using Eagles.Application.Model.Column.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.App;

namespace Eagles.Interface.Core.DataBase
{
    public interface IColumnDataAccess: IInterfaceBase
    {
        List<TbAppModule> GetColumnList(GetColumnRequset requset);
        TbAppModule GetColumnDetail(GetColumnDetailRequset requset);
        int RemoveColumn(RemoveColumnRequset requset);
        int EditColumn(TbAppModule mod);
        int CreateColumn(TbAppModule mod);
    }
}