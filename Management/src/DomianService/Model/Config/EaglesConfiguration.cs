﻿using System.Xml.Serialization;
using Eagles.Base.Configuration;

namespace Eagles.DomainService.Model.Config
{
    [XmlRoot("Eagles")]
    [XmlPath("/Configuration/Eagles.config")]
    public class EaglesConfiguration
    {
        /// <summary>
        /// 登陆失败次数
        /// </summary>
        public int LoginErrorCount { get; set; }

        /// <summary>
        /// 失败多少次后 需要验证码 验证
        /// </summary>
        public int CheckVerificationCode { get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
        public double LockingTime { get; set; }

        /// <summary>
        /// 图片存放目录
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 图片网络url
        /// </summary>
        public string ImageBaseUrl { get; set; }

        /// <summary>
        /// 导出文件路径
        /// </summary>
        public string ExportPath { get; set; }
    }
}
