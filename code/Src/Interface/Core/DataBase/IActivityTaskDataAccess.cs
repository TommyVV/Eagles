﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Base;
using Eagles.DomainService.Model.Activity;

namespace Eagles.Interface.Core.DataBase
{
   public interface IActivityTaskDataAccess: IInterfaceBase
    {
        int EditActivity(TB_ACTIVITY mod);
        int CreateActivity(TB_ACTIVITY mod);
    }
}
