using System.Collections.Generic;

namespace Eagles.Application.Model.GetMenu
{
    /// <summary>
    /// 菜单查询
    /// </summary>
    public class GetMenuResponse
    {
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<AppMenu> AppMenus { get; set; }

        /// <summary>
        /// 机构图片
        /// </summary>
        public string LogoUrl { get; set; }

    }
}