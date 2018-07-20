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

        /// <summary>
        /// 导出文件路径
        /// </summary>
        public int TokenExpTime { get; set; }

        /// <summary>
        /// AccessKeyId
        /// </summary>
        public int AccessKeyId { get; set; }

        /// <summary>
        /// AccessKeySecret
        /// </summary>
        public int AccessKeySecret { get; set; }

        /// <summary>
        /// 短信签名-可在短信控制台中找到
        /// </summary>
        public int SignName { get; set; }

        /// <summary>
        /// 短信模板-可在短信控制台中找到
        /// </summary>
        public int TemplateCode { get; set; }

        /// <summary>
        /// 短信签名-可在短信控制台中找到
        /// </summary>
        public int TemplateParam { get; set; }
    }
}