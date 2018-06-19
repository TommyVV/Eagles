using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.AppModel.GetMenu
{
    /// <summary>
    /// 菜单查询
    /// </summary>
    public class GetMenuResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 一级菜单
        /// </summary>
        public List<Menu> FirstMenus { get; set; }

        /// <summary>
        /// 二级菜单
        /// </summary>
        public Menu SecondMenus { get; set; }
        
    }
}