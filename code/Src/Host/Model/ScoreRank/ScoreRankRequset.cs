using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ScoreRank
{
    public class ScoreRankRequset:RequestBase
    {
        /// <summary>
        /// 机构号
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string UserName { get; set; }
    }
}
