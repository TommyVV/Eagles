using System.Collections.Generic;
using Eagles.Application.Model.DeliverGoods.Model;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.DeliverGoods.Requset
{
    /// <summary>
    /// 
    /// </summary>
    public class EditDeliverGoodsRequest : RequestBase
    {
        /// <summary>
        /// 订单主键
        /// </summary>
        public List<AddressInfo> OrderInfo { get; set; }


        ///// <summary>
        ///// 
        ///// </summary>
        //public DeliverStatus DeliverStatus { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
