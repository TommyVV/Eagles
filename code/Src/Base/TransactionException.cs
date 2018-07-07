using System;
using Eagles.Application.Model;

namespace Eagles.Base
{
    public class TransactionException : Exception
    {
        public string ErrorMessage { get; set; }

        public string ErrorCode { get; set; }

        public TransactionException(string errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}
