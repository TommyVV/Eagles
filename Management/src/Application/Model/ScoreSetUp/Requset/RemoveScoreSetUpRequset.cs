using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.ScoreSetUp.Requset
{
    public class RemoveScoreSetUpRequset : RequestBase
    {
       /// <summary>
       /// 积分奖励id 后台主键
       /// </summary>
        public int ScoreSetUpId { get; set; }

    }
}
