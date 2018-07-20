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
        /// 积分奖励唯一id
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
        ///0:发表文章奖励
        ///1:文章字数奖励
        ///2:文章关键字奖励
        ///10:参加活动奖励
        ///11:活动分享到支部奖励
        ///12:活动分享到组织奖励
        ///20:任务完成奖励
        ///21:任务分享到支部奖励
        ///22:任务分享到组织奖励
        ///30:会议文章奖励
        ///40:心得体会类型奖励
        /// </summary>
        public int RewardType { get; set; }

        /// <summary>
        /// 字数
        /// </summary>
        public int WordCount { get; set; }
    }
}