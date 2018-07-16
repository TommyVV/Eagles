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
        /// 机构名称（新增修改，无需传入）
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 菜单id（新增无需传入，修改传入）
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// 级菜单名称
        /// </summary>
        public string MenuName { get; set; }

        /// <summary>
        /// 菜单跳转链接
        /// </summary>
        public string MenuLink { get; set; }

        /// <summary>
        /// 菜单等级；暂时只有2级（1,2） 
        /// </summary>
        public string MenuLevel { get; set; }

        /// <summary>
        /// 父菜单id 新建二级菜单时必须传 
        /// </summary>
        public int ParentId { get; set; }

    }

}
