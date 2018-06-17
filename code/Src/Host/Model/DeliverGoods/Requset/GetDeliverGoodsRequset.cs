using System;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.DeliverGoods.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class GetDeliverGoodsRequset : OrgListRequestBase
    {
        /// <summary>
        /// 商品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DeliverStatus DeliverStatus { get; set; }
    }
}
