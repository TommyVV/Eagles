namespace Eagles.Application.Model.RollImage.Requset
{
   /// <summary>
   /// 
   /// </summary>
   public  class GetRollImageDetailRequset:RequestBase
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
