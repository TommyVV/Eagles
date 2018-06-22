using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.ScoreSetUp.Requset
{
    public class GetScoreSetUpRequset : OrgListRequestBase
    {
        public OperationType OperationType { get; set; }

    }
}
