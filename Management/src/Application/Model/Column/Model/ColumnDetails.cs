namespace Eagles.Application.Model.Column.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ColumnInfoDetails : ColumnInfo
    {

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsSetTop { get; set; }

        /// <summary>
        /// 小图
        /// </summary>
        public string ColumnIcon { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        public string ColumnImg { get; set; }

        /// <summary>
        /// 所属页面（0:首页;1:党建门户;2:党务工作;3:党建学习
        /// </summary>
        public int ModuleType { get; set; }
    }
}
