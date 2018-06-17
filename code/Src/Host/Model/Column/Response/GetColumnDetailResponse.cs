using Eagles.Application.Model.Column.Model;

namespace Eagles.Application.Model.Column.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetColumnDetailResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ColumnInfoDetails Info { get; set; }
    }
}
