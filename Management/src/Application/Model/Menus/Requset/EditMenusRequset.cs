namespace Eagles.Application.Model.Menus.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditMenusRequset:RequestBase
    {    
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public Model.Menus Info { get; set; }
    }
}
