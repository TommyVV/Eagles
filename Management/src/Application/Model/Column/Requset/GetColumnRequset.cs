using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.Column.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetColumnRequset : ListRequestBase
    {

        /// <summary>
        /// 任务/活动 名字
        /// </summary>
        public int ModulType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }

    }
}