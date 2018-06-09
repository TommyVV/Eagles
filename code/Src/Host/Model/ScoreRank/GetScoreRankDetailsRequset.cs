using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ScoreRank
{
    public class GetScoreRankDetailsRequset
    {
        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
    }
}
