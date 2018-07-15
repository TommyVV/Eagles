
namespace Eagles.Application.Model.TestPaper.GetIsJoinTest
{
    /// <summary>
    /// 试卷完成接口
    /// </summary>
    public class GetIsJoinTestRequest : RequestBase
    {
        /// <summary>
        /// 试卷编号
        /// </summary>
        public int TestId { get; set; }

        /// <summary>
        /// 活动编号
        /// </summary>
        public int ActivityId { get; set; }
    }
}