﻿using Eagles.Application.Model.SystemMessage.Model;

namespace Eagles.Application.Model.SystemMessage.Response
{
    public class GetSystemMessageInfoDetailResponse : ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public SystemMessageInfoDetails info { get; set; }
    }
}