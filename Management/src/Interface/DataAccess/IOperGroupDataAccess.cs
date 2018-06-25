using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.AuthorityGroup.Requset;
using Eagles.Application.Model.AuthorityGroupSetUp.Requset;
using Eagles.Application.Model.Operator.Requset;
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
        List<TbAuthority> GetAuthorityGroupSetUp(GetAuthorityGroupSetUpRequest requset);
        int RemoveAuthorityGroupSetUp(int groupId);
        int CreateAuthorityGroupSetUp(List<TbAuthority> requset);
    }
}
