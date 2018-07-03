using System;

namespace Eagles.Application.Model
{
    /// <summary>
    /// 基本的返回
    /// </summary>
    public class ResponseBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ResponseBase()
        {
            IsSuccess = true;
            Code = MessageCode.Success;
            Message = MessageKey.Success;
        }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime DateTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 返回码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }
    }
}
