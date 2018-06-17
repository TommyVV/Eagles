using Eagles.Application.Model.Meeting.Model;

namespace Eagles.Application.Model.Meeting.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditMeetingRequset : RequestBase
    {
      
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public MeetingDetail Info { get; set; }
    }
}
