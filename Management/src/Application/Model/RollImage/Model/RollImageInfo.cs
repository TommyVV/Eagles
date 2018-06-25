using System.Collections.Generic;

namespace Eagles.Application.Model.RollImage.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RollImageInfo
    {
        ///// <summary>
        ///// 滚动图片组ID
        ///// </summary>
        //public int RollImageId { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }

        ///// <summary>
        ///// 机构名称
        ///// </summary>
        //public string OrgName { get; set; }

        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 页面id
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// 滚动图片url
        /// </summary>
        public string Img { get; set; }
    }
}
