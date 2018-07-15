using System;
using System.Collections.Generic;
using Eagles.Application.Model.Enums;

namespace Eagles.Application.Model.DeliverGoods.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class DeliverGoods
    {

        /// <summary>
        /// 订单主键
        /// </summary>
        public int OrderId { get; set; }
       
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///商品名
        /// </summary>
        public string GoodsName { get; set; }

        /// <summary>
        ///下单时间
        /// </summary>
        public string PlaceTime { get; set; }



        /// <summary>
        /// 快递id
        /// </summary>
        public string ExpressId { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public DeliverStatus DeliverStatus { get; set; }

        ///// <summary>
        ///// 备注
        ///// </summary>
        //public string  Remark { get; set; }


        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        ///// <summary>
        ///// 创建时间
        ///// </summary>
        //public string CreateTime { get; set; }
       

        /// <summary>
        /// 支付积分
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 订单状态;
        /// 0:成功
        ///1:失败
        /// </summary>
        public int OrderStatus { get; set; }
    }


    /// <summary>
    /// 详情
    /// </summary>
    public class DeliverGoodsDeatil:DeliverGoods
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }


        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }
    }
}
