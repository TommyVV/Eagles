using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.AppModel.Activity.GetActivityDetail
{
    /// <summary>
    /// 活动详情查询
    /// </summary>
    public class GetActivityDetailResponse : ResponseBase
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
        /// 发起用户编号
        /// </summary>
        public string InitiateEncryptUserid { get; set; }

        /// <summary>
        /// 接受用户编号
        /// </summary>
        public string AcceptEncryptUserid { get; set; }

        /// <summary>
        /// 附件集合
        /// </summary>
        public List<Attachment> AttachmentList { get; set; }
    }
}