using Eagles.Application.Model.Goods.Model;

namespace Eagles.Application.Model.Goods.Response
{
    /// <summary>
    /// 
    /// </summary>
    public class GetGoodsDetailResponse : ResponseBase
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public GoodsDetail Info { get; set; }
    }
}
