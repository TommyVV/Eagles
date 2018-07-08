
namespace Eagles.DomainService.Model.App
{
    /// <summary>
    /// TB_APP_MODULE
    /// </summary>
    public class TbAppModule
    {
        /// <summary>
        /// 大图
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 是否在首页显示
        /// </summary>
        public int IndexDisplay { get; set; }
        /// <summary>
        /// 首页显示数量
        /// </summary>
        public int IndexPageCount { get; set; }
        /// <summary>
        ///菜单id 
        /// </summary>
        public int ModuleId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        ///类型;
        ///0:首页;
        ///1:党建门户;
        ///2:党务工作;
        /// 3:党建学习'
        /// </summary>
        public int ModuleType { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 小图
        /// </summary>
        public string SmallImageUrl { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string TargetUrl { get; set; }
    }
}