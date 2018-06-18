using Eagles.Application.Model.News.Model;

namespace Eagles.Application.Model.News.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditNewRequset:RequestBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public NewDetail Info { get; set; }
    }
}
