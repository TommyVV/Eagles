using Eagles.Application.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ScoreRank
{
    public class ScoreRankInfo
    {

        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public string Score { get; set; }
        /// <summary>
        /// 用户使用积分
        /// </summary>
        public string UseScore { get; set; }

        /// <summary>
        /// 用户身份
        /// </summary>
        public UserIdentity UserIdentity { get; set; }

    }
}
