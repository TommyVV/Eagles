﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eagles.Application.Model.RollImage
{
    public class AlterRollImageInfoRequest
    {
        /// <summary>
        /// 维护接口 传主键id 表示修改 不传表示新增
        /// </summary>
        public RollImageInfo info { get; set; }

        /// <summary>
        /// 凭证
        /// </summary>
        public string Token { get; set; }
    }
}