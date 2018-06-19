﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Org;

namespace Eagles.Interface.Core.DataBase
{
    public interface IOrganizationDataAccess : IInterfaceBase
    {
        List<TB_ORG_INFO> GetOrganizationList(GetOrganizationRequset requset);
        TB_ORG_INFO GetOrganizationDetail(GetOrganizationDetailRequset requset);
        int RemoveOrganization(RemoveOrganizationRequset requset);
        int EditOrganization(TB_ORG_INFO mod);
        int CreateOrganization(TB_ORG_INFO mod);
    }
}
