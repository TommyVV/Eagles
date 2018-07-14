using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model;
using Eagles.Application.Model.Branch.Requset;
using Eagles.Application.Model.Branch.Response;
using Eagles.Base;

namespace Eagles.Interface.Core
{
    public interface IBranchHandler : IInterfaceBase
    {
        bool EditBranch(EditBranchRequset requset);

        bool RemoveBranch(RemoveBranchRequset requset);

        GetBranchResponse Branch(GetBranchRequset requset);

        GetBranchDetailResponse GetBranchDetail(GetBranchDetailRequset requset);
    }
}
