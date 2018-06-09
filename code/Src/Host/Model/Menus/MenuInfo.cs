using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Menus
{
    public class MenuInfo
    {



        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }


        /// <summary>
        /// 级菜单名称
        /// </summary>
        public string MenuId { get; set; }

        /// <summary>
        /// 级菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 级菜单链接
        /// </summary>
        public string MenuLink { get; set; }

        /// <summary>
        /// 菜单级别 
        /// </summary>
        public MenuLevel MenuLevel { get; set; }

        /// <summary>
        /// 父菜单id 新建二级菜单时必须传 
        /// </summary>
        public string ParentId { get; set; }

    }

}
