using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Menus.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMenusRequset : OrgListRequestBase
    {

        /// <summary>
        /// 菜单级别 
        /// </summary>
        public MenuLevel MenuLevel { get; set; }
    }
}
