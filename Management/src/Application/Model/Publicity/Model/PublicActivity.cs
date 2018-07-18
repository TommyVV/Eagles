using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Publicity.Model
{
    public class PublicActivity
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
        /// 负责人
        /// </summary>
        public string ResponsibleUserName { get; set; }

        /// <summary>
        /// 申请时间 格式 yyyy-MM-dd HH：mm：ss
        /// </summary>
        public string CreateTime { get; set; }

        /// <summary>
        /// 参与人数
        /// </summary>
        public int UserCount { get; set; }
    }
}
