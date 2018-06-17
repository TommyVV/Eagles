using System;

namespace Eagles.Application.Model.Column.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ColumnInfoDetails : ColumnInfo
    {
        ///// <summary>
        ///// 生效时间
        ///// </summary>
        //public DateTime EnableTime { get; set; }

        ///// <summary>
        ///// 内容 json格式 图片 文字
        ///// </summary>
        //public string Content { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsSetTop { get; set; }


        ///// <summary>
        ///// 所属页面
        ///// </summary>
        //public int PageId { get; set; }

        /// <summary>
        /// 小图
        /// </summary>
        public string ColumnIcon { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        public string ColumnImg { get; set; }

        ///// <summary>
        ///// 菜单id
        ///// </summary>
        //public int ModuleId { get; set; }

        ///// <summary>
        ///// 菜单名称
        ///// </summary>
        //public string ModuleName { get; set; }
        /// <summary>
        ///  '类型;
        /// </summary>
        public int ModuleType { get; set; }
    }
}
