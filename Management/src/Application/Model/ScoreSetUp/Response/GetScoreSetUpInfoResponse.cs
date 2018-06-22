﻿using System.Collections.Generic;
using Eagles.Application.Model.ScoreSetUp.Model;

namespace Eagles.Application.Model.ScoreSetUp.Response
{
    public class GetScoreSetUpInfoResponse : ResponseBase
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }
        /// <summary>
        /// 集合
        /// </summary>
        public List<ScoreSetUpInfo> List { get; set; }
    }
}
