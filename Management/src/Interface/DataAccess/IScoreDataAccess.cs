using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.ScoreSetUp.Requset;
using Eagles.Base;
using Eagles.DomainService.Model.Score;

namespace Eagles.Interface.DataAccess
{
    public interface IScoreDataAccess: IInterfaceBase
    {
        int EditScoreSetUp(TbRewardScore mod);
        int CreateScoreSetUp(TbRewardScore mod);
        int RemoveScoreSetUp(RemoveScoreSetUpRequset requset);
        TbRewardScore GetScoreSetUpDetail(GetScoreSetUpDetailRequset requset);
        List<TbRewardScore> GetScoreSetUps(GetScoreSetUpRequset requset, out int totalCount);


    }
}
