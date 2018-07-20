namespace Eagles.Application.Model.Publicity.Request
{
    public class GetPublicArticleDetailRequest:RequestBase
    {
        /// <summary>
        /// 公开文章id
        /// </summary>
        public int NewsId { get; set; }
    }
}
