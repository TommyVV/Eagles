namespace Eagles.Application.Model.News.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetNewDetailRequset : RequestBase
    {
        /// <summary>
        /// 新闻名字
        /// </summary>
        public int NewsId { get; set; }
    }
}
