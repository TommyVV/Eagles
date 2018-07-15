using System.Collections.Generic;
using Eagles.Application.Model.Branch.Requset;
using Eagles.Application.Model.Organization.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Org;

namespace Eagles.Interface.DataAccess
{
    public interface IBranchDataAccess : IInterfaceBase
    {
        int EditBranch(TbBranch mod);
        int CreateBranch(TbBranch mod);
        int RemoveBranch(RemoveBranchRequset requset);
        List<TbBranch> GetBranchList(GetBranchRequset requset, out int totalcount);
        TbBranch GetBranchDetail(GetBranchDetailRequset requset);
    }
}
