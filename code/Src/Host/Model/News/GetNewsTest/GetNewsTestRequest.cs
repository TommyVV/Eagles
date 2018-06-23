namespace Eagles.Application.Model.News.GetNewsTest
{
    /// <summary>
    /// 新闻试卷查询
    /// </summary>
    public class GetNewsTestRequest : RequestBase
    {
        /// <summary>
        /// 试题编号
        /// </summary>
        public int TestId { get; set; }
    }
}