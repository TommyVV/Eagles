namespace Eagles.Application.Model.RollImage.Requset
{
    public class RemoveRollImageRequset : RequestBase
    {
        /// <summary>
        /// 页面ID
        /// </summary>
        public string PageId { get; set; }

        /// <summary>
        /// 机构号
        /// </summary>
        public int OrgId { get; set; }
    }
}
