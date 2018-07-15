using System.Collections.Generic;
using Eagles.Application.Model.AuthorityGroup.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Authority;
using Eagles.DomainService.Model.Oper;

namespace Eagles.Interface.DataAccess
{
    public interface IOperGroupDataAccess : IInterfaceBase
    {
        int EditOperGroup(TbOperGroup mod);
        int CreateOperGroup(TbOperGroup mod);
        int RemoveOperGroup(RemoveAuthorityGroupInfoRequset requset);
        TbOperGroup GetOperGroupDetail(GetAuthorityGroupDetailRequset requset);
        List<TbOperGroup> GetOperGroupList(GetAuthorityGroupRequset requset);
        List<TbAuthority> GetAuthorityGroupSetUp(int groupId);
        int RemoveAuthorityGroupSetUp(int groupId);
        int CreateAuthorityGroupSetUp(List<TbAuthority> requset);
    }
}
