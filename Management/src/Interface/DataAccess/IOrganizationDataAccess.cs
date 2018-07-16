using System.Collections.Generic;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Org;

namespace Eagles.Interface.DataAccess
{
    public interface IOrganizationDataAccess : IInterfaceBase
    {
        List<TbOrgInfo> GetOrganizationList(GetOrganizationRequset requset,out int totalCount);
        TbOrgInfo GetOrganizationDetail(GetOrganizationDetailRequset requset);
        int RemoveOrganization(RemoveOrganizationRequset requset);
        int EditOrganization(TbOrgInfo mod);
        int CreateOrganization(TbOrgInfo mod);
        List<TbOrgInfo> GetOrganizationList(List<int> list);
    }
}
