
namespace Eagles.Application.Model.Scroll.GetScrollImg
{
    /// <summary>
    /// 滚动图片查询
    /// </summary>
    public class GetScrollImgRequest : RequestBase
    {
        /// <summary>
        /// 页面类型
        /// </summary>
        public string PageType { get; set; }
    }
}