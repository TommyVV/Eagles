using System;

namespace Eagles.Application.Model
{
    public class ResponseBase
    {
        public DateTime DateTime { get; set; }

        public string ErrorCode { get; set; }

        public string Message { get; set; }

        public string IsSuccess { get; set; }
    }
}
