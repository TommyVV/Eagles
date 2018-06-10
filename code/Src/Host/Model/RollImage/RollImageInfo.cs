using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.RollImage
{
    public class RollImageInfo
    {
        /// <summary>
        /// 滚动图片组ID
        /// </summary>
        public int RollImageId { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public string OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }


        /// <summary>
        /// 页面ID
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// 页面名称
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// 滚动图片组
        /// </summary>
        public List<string> Img { get; set; }
    }
}
