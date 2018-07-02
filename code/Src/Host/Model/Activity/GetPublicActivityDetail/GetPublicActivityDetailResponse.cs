using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Activity.GetPublicActivityDetail
{
    /// <summary>
    /// 公开活动详情查询
    /// </summary>
    public class GetPublicActivityDetailResponse : ResponseBase
    {
        /// <summary>
        /// 活动Id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 活动图片Url
        /// </summary>
        public string ActivityImageUrl { get; set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        public string ActivityContent { get; set; }

        /// <summary>
        /// 活动参与人数集合
        /// </summary>
        public List<string> ActivityJoinPeopleList { get; set; }

        /// <summary>
        /// 活动状态
        /// </summary>
        public int ActivityStatus { get; set; }

        /// <summary>
        /// 发起用户编号
        /// </summary>
        public string InitiateEncryptUserId { get; set; }

        /// <summary>
        /// 接受用户编号
        /// </summary>
        public string AcceptEncryptUserId { get; set; }

        /// <summary>
        /// 创建类型
        /// </summary>
        public int CreateType { get; set; }

        /// <summary>
        /// 附件集合
        /// </summary>
        public List<Attachment> AttachmentList { get; set; }
    }
}