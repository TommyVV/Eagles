namespace Eagles.Application.Model.GetMenu
{
    public class AppSubMenu
    {
        /// <summary>
        /// 菜单Id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public int ParentMenuId { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string TargetUrl { get; set; }

    }
}
