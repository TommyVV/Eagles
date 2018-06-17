using Eagles.Application.Model.Column.Model;

namespace Eagles.Application.Model.Column.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditColumnRequset : RequestBase
    {

        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public ColumnInfoDetails Info { get; set; }

    }
}
