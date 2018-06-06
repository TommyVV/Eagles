using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Curd
{
    class IntegralRankQueryResponse : ResponseBase
    {
        public List<RankGroup> list { get; set; }

        
    }

    class RankGroup
    {
        string Rank;
        string Name;

    }
}
