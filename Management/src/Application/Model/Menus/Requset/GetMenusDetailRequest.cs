namespace Eagles.Application.Model.Menus.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMenusDetailRequest : RequestBase
    {
        /// <summary>
        /// 级菜单名称
        /// </summary>
        public int MenuId { get; set; }
    }
}
