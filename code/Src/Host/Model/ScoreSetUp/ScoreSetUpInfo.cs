using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.enums;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreSetUp
{
    public class ScoreSetUpInfo
    {
        public OperationType OperationType { get; set; }

        public int ScoreSetUpId { get; set; }

        /// <summary>
        /// 积分奖励值
        /// </summary>
        public int Score { get; set; }

        //状态
        public Status Status { get; set; }

        /// <summary>
        /// 文章类型 奖励关键字
        /// </summary>
        public List<String> Keyword { get; set; }

        /// <summary>
        /// 可获取积分次数
        /// </summary>
        public int ScoreCount { get; set; }
    }
}
