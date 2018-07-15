using System;
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
        /// 
        /// </summary>
       // public OperationType OperationType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int ScoreSetUpId { get; set; }

        /// <summary>
        /// 积分奖励值
        /// </summary>
        public int Score { get; set; }

        //状态
        /// <summary>
        /// 
        /// </summary>
    //    public Status Status { get; set; }

        /// <summary>
        /// 文章类型 奖励关键字
        /// </summary>
        public List<String> Keyword { get; set; }

        /// <summary>
        /// 可获取积分次数
        /// </summary>
     //   public int ScoreCount { get; set; }

        /// <summary>
        /// 学习时间
        /// </summary>
        public int LearnTime { get; set; }

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
        /// 字数
        /// </summary>
        public int WordCount { get; set; }
    }
}