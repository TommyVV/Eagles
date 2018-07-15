namespace Eagles.Application.Model.TestPaper.GetTestPaper
{
    /// <summary>
    /// 试卷查询
    /// </summary>
    public class GetTestPaperRequest : RequestBase
    {
        /// <summary>
        /// 试题编号
        /// </summary>
        public int TestId { get; set; }
    }
}