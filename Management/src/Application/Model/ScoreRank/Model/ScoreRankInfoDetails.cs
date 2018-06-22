using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreRank.Model
{
    /// <summary>
    /// 
    /// </summary>
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

       
        /// <summary>
        /// 
        /// </summary>
        public OperationType OperationType { get; set; }
    }
}
