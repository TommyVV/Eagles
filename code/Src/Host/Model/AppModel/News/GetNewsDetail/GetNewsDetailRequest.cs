
namespace Eagles.Application.Model.AppModel.News.GetNewsDetail
{
    /// <summary>
    /// 新闻详情查询
    /// </summary>
    public class GetNewsDetailRequest : RequestBase
    {
        /// <summary>
        /// 新闻编号
        /// </summary>
      public int NewsId { get; set; }
    }
}