using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ScoreRank.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class UserScoreTrace
    {
        /// <summary>
        /// 描述
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }

        ///// <summary>
        ///// 组织id
        ///// </summary>
        //public int OrgId { get; set; }

        ///// <summary>
        ///// 原积分
        ///// </summary>
        //public int OriScore { get; set; }

        /// <summary>
        /// 积分奖励类型;
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
        public string RewardsType { get; set; }

        /// <summary>
        /// 获得积分
        /// </summary>
        public int Score { get; set; }

        ///// <summary>
        ///// 流水号
        ///// </summary>
        //public int TraceId { get; set; }
        
    }
}
