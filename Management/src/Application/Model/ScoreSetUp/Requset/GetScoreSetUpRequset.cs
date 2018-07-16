using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreSetUp.Requset
{
    public class GetScoreSetUpRequset : ListRequestBase
    {
        /// <summary>
        /// 积分奖励类型
        /// </summary>
        public int OperationType { get; set; }

    }
}
