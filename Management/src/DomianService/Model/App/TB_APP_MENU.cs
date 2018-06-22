using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.DomainService.Model.App
{
    public class TB_APP_MENU
    {
        /// <summary>
        /// 等级
        /// </summary>
        public MenuLevel Level { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { get; set; }
        /// <summary>
        /// 组织id
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 上级菜单id
        /// </summary>
        public int ParentMenuId { get; set; }
        /// <summary>
        /// 跳转链接
        /// </summary>
        public string TragetUrl { get; set; }
    }
}
