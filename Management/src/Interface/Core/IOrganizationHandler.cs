using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Application.Model.Organization.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IOrganizationHandler:IInterfaceBase
    {
        ResponseBase EditOrganization(EditOrganizationRequset requset);

        ResponseBase RemoveOrganization(RemoveOrganizationRequset requset);

        GetOrganizationResponse Organization(GetOrganizationRequset requset);

        GetOrganizationDetailResponse GetOrganizationDetail(GetOrganizationDetailRequset requset);
    }
}
