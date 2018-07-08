using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Menus.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMenusRequset : ListRequestBase
    {

        /// <summary>
        /// 菜单级别 
        /// </summary>
        public MenuLevel MenuLevel { get; set; }
    }
}
