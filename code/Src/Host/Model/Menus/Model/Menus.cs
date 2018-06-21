using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Menus.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Menus
    {
        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }
        /// <summary>
        /// 级菜单名称
        /// </summary>
        public int MenuId { get; set; }

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
        public int ParentId { get; set; }

    }

}
