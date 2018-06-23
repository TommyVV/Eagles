using System.Collections.Generic;

namespace Eagles.Application.Model.GetMenu
{
    public class AppMenu
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
        /// 跳转链接
        /// </summary>
        public string TargetUrl { get; set; }

        /// <summary>
        /// 二级菜单
        /// </summary>
        public List<AppSubMenu> SubMenus { get; set; }

        /// <summary>
        /// 是否含有下级菜单
        /// </summary>
        public bool HasSubMenu { get; set; }
    }
}
