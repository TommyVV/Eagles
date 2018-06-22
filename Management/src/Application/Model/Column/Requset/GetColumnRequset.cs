using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Column.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetColumnRequset : OrgListRequestBase
    {

        /// <summary>
        /// 任务/活动 名字
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

    }
}