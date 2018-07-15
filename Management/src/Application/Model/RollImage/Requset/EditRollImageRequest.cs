using Eagles.Application.Model.RollImage.Model;

namespace Eagles.Application.Model.RollImage.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditRollImageRequest: ListRequestBase
    {
        ///// <summary>
        ///// 页面ID
        ///// </summary>
        //public string PageId { get; set; }
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public RollImageInfo Info { get; set; }
    }
}
