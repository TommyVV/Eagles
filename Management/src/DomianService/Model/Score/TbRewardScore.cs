using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.DomainService.Model.Score
{
    public class TbRewardScore
    {
        /// <summary>
        /// 奖励id
        /// </summary>
        public int BranchId { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string KeyWord { get; set; }
        /// <summary>
        /// 学习时间;单位:分钟
        /// </summary>
        public int LearnTime { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 奖励id
        /// </summary>
        public int RewardId { get; set; }
        /// <summary>
        /// 奖励类型;
        ///0:任务奖励
        ///1:活动奖励;
        ///2:字数奖励
        ///3:关键字奖励
        ///4:学习时间奖励
        /// </summary>
        public int RewardType { get; set; }
        /// <summary>
        /// 奖励积分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 字数
        /// </summary>
        public int WordCount { get; set; }
    }
}
