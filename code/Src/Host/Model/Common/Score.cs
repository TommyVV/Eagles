using System;
namespace Eagles.Application.Model.Common
{
    public class ScoreExchange
    {

        /// <summary>
        /// 兑换积分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 兑换时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 积分奖励类型
        /// </summary>
        public string RewardsType { get; set; }

        /// <summary>
        /// 原积分
        /// </summary>
        public int OriScore { get; set; }

        /// <summary>
        /// 积分描述
        /// </summary>
        public string Comment { get; set; }

    }
}