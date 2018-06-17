namespace Eagles.Application.Model.News.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetNewsDetailRequset : RequestBase
    {
        /// <summary>
        /// 新闻名字
        /// </summary>
        public string NewsId { get; set; }
    }
}
