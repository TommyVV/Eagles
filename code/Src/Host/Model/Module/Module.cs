namespace Eagles.Application.Model.Module
{
    public class Module
    {
        /// <summary>
        /// 栏目板块Id
        /// </summary>
        public int ModuleId { get; set; }

        /// <summary>
        /// 栏目名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 跳转链接,如果为空,跳转到通用列表页
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// 首页显示条数
        /// </summary>
        public int IndexPageCount { get; set; }
        
        /// <summary>
        /// 小图
        /// </summary>
        public string SmallImageUrl { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        //public List<NewsInfo> News { get; set; }

    }
}
