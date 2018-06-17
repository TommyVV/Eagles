using System.Collections.Generic;

namespace Eagles.Application.Model.Meeting.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetMeetingResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<Model.Meeting> List { get; set; }
    }
}
