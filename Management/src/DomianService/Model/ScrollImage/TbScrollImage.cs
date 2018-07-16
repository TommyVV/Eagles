namespace Eagles.DomainService.Model.ScrollImage
{
    public class TbScrollImage
    {
        public int Id { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { get; set; }
        /// <summary>
        /// 机构类型
        /// </summary>
        public int OrgId { get; set; }
        /// <summary>
        /// 页面类型;
        ///0:首页;
        ///1:党建门户;
        ///2:党务工作;
        ///3:党建学习'
        /// </summary>
        public string PageType { get; set; }

        public string TargetUrl { get; set; }
    }
}
