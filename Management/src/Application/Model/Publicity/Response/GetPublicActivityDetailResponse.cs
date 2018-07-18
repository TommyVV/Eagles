using System.Collections.Generic;
using Eagles.Application.Model.Common;

namespace Eagles.Application.Model.Publicity.Response
{
    public class GetPublicActivityDetailResponse
    {
        /// <summary>
        /// 活动id
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public string FromUser { get; set; }

        /// <summary>
        /// 负责人
        /// </summary>
        public string ResponsibleUserName { get; set; }

        /// <summary>
        /// 发布时间（有的地方叫申请时间，一个意思） 格式 yyyy-MM-dd HH：mm：ss
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 活动内容
        /// </summary>
        public string ActivityContent { get; set; }

        /// <summary>
        /// 活动反馈
        /// </summary>
        public string ActivityFeedBack{ get; set; }

        /// <summary>
        /// 附件
        /// </summary>
        public List<Attachment> Attachments { get; set; }
    }
}
