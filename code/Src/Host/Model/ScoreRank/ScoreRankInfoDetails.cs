using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreRank
{
    public class ScoreRankInfoDetails: ScoreRankInfo
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 积分使用情况
        /// </summary>
        public string ScoreRecord { get; set; }

       
        public OperationType OperationType { get; set; }
    }
}
