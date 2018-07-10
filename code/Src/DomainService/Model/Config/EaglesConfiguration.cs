using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.DomainService.Model.Config
{
    [XmlRoot("Eagles")]
    [XmlPath("/Configuration/Eagles.config")]
    public class EaglesConfiguration
    {
        public string FilePath { get; set; }

        public string ImageBaseUrl { get; set; }

        /// <summary>
        /// 活动通知跳转链接
        /// </summary>
        public string ActivityNoticeUrl { get; set; }

        /// <summary>
        /// 任务通知跳转链接
        /// </summary>
        public string TaskNoticeUrl { get; set; }
    }
}
