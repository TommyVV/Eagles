namespace Eagles.Application.Model.Meeting.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class MeetUser
    {
        /// <summary>
        /// 会议参与人姓名
        /// </summary>
        public string MeetUserName { get; set; }

        /// <summary>
        /// 会议参与人联系电话
        /// </summary>
        public string MeetUserPhone { get; set; }
        
        /// <summary>
        /// 导入校验错误信息
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}
