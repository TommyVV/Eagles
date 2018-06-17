﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.Goods
{
    public class GoodsInfoDetails: GoodsInfo
    {

        /// <summary>
        /// 已售
        /// </summary>
        public int Sale { get; set; }

        /// <summary>
        /// 销售开始时间
        /// </summary>
        public DateTime SellStartTime { get; set; }

        /// <summary>
        /// 销售结束时间
        /// </summary>
        public DateTime SellEndTime { get; set; }

        /// <summary>
        /// 每人最大兑换数量
        /// </summary>
        public int MaxExchangeNum { get; set; }

        /// <summary>
        /// 参考价格
        /// </summary>
        public int ReferePrice{ get; set; }

        /// <summary>
        /// 内容 json格式 图片 文字
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 小图
        /// </summary>
        public string ColumnIcon { get; set; }

        /// <summary>
        /// 大图
        /// </summary>
        public string ColumnImg { get; set; }

    }
}