namespace Eagles.Application.Model.News.AddNewsViewCount
{
    public class AddNewsCountRequest:RequestBase
    {
        /// <summary>
        /// 新闻id
        /// </summary>
        public int NewsId { get; set; }
    }
}
