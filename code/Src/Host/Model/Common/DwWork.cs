using System;

namespace Eagles.Application.Model.Common
{
    /// <summary>
    /// 党务工作
    /// </summary>
    public class DwWork
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// 小图Url
        /// </summary>
        public string SmallImgUrl { get; set; }

        /// <summary>
        /// 大图Url
        /// </summary>
        public string ImgUrl { get; set; }
        
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 首页显示数量
        /// </summary>
        public int IndexPageNo { get; set; }

        /// <summary>
        /// 是否在首页显示
        /// </summary>
        public int IndexDisplay { get; set; }
    }
}