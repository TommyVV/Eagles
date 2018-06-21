﻿using System;
using System.Collections.Generic;

namespace Eagles.Application.Model.AppModel.Product.GetProduct
{
    /// <summary>
    /// 获取积分商品
    /// </summary>
    public class GetProductResponse : ResponseBase
    {
        /// <summary>
        /// 商品列表
        /// </summary>
        public List<Common.Product> ProductList { get; set; }
    }
}