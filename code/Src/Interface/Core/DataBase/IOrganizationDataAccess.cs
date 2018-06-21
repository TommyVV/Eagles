using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Org;
using Eagles.DomainService.Model.PartyMember;

namespace Eagles.Interface.Core.DataBase
{
    public interface IOrganizationDataAccess : IInterfaceBase
    {
        List<TbOrgInfo> GetOrganizationList(GetOrganizationRequset requset);
        TbOrgInfo GetOrganizationDetail(GetOrganizationDetailRequset requset);
        int RemoveOrganization(RemoveOrganizationRequset requset);
        int EditOrganization(TbOrgInfo mod);
        int CreateOrganization(TbOrgInfo mod);
     //   List<TB_USER_RELATIONSHIP> GetOrganizationList(List<Application.Model.PartyMember.Model.UserInfoDetails> list);
        List<TbOrgInfo> GetOrganizationList(List<int> list);
    }
}
