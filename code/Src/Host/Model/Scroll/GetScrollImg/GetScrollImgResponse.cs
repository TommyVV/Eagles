using System.Collections.Generic;

namespace Eagles.Application.Model.Scroll.GetScrollImg
{
    /// <summary>
    /// 滚动图片查询
    /// </summary>
    public class GetScrollImgResponse : ResponseBase
    {
        /// <summary>
        /// 页面类型;0:首页;1:党建门户;2:党务工作;3:党建学习
        /// </summary>
        public string PageType { get; set; }
        
        /// <summary>
        /// 滚动图片Url
        /// </summary>
        public List<RollImage> ImageList { get; set; }
    }

    /// <summary>
    /// 图片
    /// </summary>
    public class RollImage
    {
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string TargetUrl { get; set; }
    }
}