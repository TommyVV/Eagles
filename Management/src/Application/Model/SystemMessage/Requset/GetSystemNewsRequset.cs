using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.SystemMessage.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetSystemNewsRequset : OrgListRequestBase
    {
        /// <summary>
        /// 标题 名字
        /// </summary>
        public string SystemMessageName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public Status Status { get; set; }
    }
}