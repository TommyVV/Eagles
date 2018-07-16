namespace Eagles.Application.Model.RollImage.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class RollImageInfo
    {

        /// <summary>
        /// 滚动图片唯一id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 机构id
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrgName { get; set; }

        /// <summary>
        /// 页面类型（0:首页; 1：党建门户:2：党务工作:3：党建学习）
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// 滚动图片url
        /// </summary>
        public string Img { get; set; }

        /// <summary>
        /// 跳转链接
        /// </summary>
        public string TargetUrl { get; set; }
    }
}
