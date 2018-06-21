using System.Collections.Generic;

namespace Eagles.Application.Model.AppModel.GetMenu
{
    /// <summary>
    /// 菜单查询
    /// </summary>
    public class GetMenuResponse : ResponseBase
    {
        /// <summary>
        /// 菜单集合
        /// </summary>
        public List<AppMenu> AppMenus { get; set; }

    }
}