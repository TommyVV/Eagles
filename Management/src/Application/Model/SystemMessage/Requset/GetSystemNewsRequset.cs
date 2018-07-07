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

        public Status Status { get; set; }
    }
}