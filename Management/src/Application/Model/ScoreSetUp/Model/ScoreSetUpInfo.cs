using System.Collections.Generic;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreSetUp.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ScoreSetUpInfo
    {
        /// <summary>
        /// 积分配置编号，新增时无需传入，修改时传入
        /// </summary>
        public int ScoreSetUpId { get; set; }

        /// <summary>
        /// 积分奖励值
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 状态字段(0:正常；1：禁用)
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// 自定义关键字奖励
        /// </summary>
        public List<string> Keyword { get; set; }

        /// <summary>
        /// 学习时间
        /// </summary>
        public int LearnTime { get; set; }

        /// <summary>
        /// 奖励类型;
        /// 0:发表文章奖励 1:文章字数奖励 2:文章关键字奖励 10:参加活动奖励
        /// 11:活动分享到支部奖励 12:活动分享到组织奖励 20:任务完成奖励
        /// 21:任务分享到支部奖励 22:任务分享到组织奖励 30:会议文章奖励 40:心得体会类型奖励
        /// </summary>
        public int RewardType { get; set; }

        /// <summary>
        /// 字数
        /// </summary>
        public int WordCount { get; set; }
    }
}