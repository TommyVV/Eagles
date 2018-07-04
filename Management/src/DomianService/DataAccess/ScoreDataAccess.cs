using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.ScoreSetUp.Requset;
using Eagles.DomainService.Model.Score;
using Eagles.Interface.DataAccess;

namespace Ealges.DomianService.DataAccess
{
    public class ScoreDataAccess: IScoreDataAccess
    {
        public int EditScoreSetUp(TbRewardScore mod)
        {
            throw new NotImplementedException();
        }

        public int CreateScoreSetUp(TbRewardScore mod)
        {
            throw new NotImplementedException();
        }

        public int RemoveScoreSetUp(RemoveScoreSetUpRequset requset)
        {
            throw new NotImplementedException();
        }

        public TbRewardScore GetScoreSetUpDetail(GetScoreSetUpDetailRequset requset)
        {
            throw new NotImplementedException();
        }

        public List<TbRewardScore> GetScoreSetUps(GetScoreSetUpRequset requset)
        {
            throw new NotImplementedException();
        }
    }
}
