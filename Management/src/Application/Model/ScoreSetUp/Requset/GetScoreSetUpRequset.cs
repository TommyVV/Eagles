using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreSetUp.Requset
{
    public class GetScoreSetUpRequset : OrgListRequestBase
    {
        /// <summary>
        /// 0:发表文章奖励
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
        public OperationType RewardType { get; set; }

    }
}
