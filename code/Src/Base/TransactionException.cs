using System;

namespace Eagles.Base
{
    public class TransactionException : Exception
    {
        private string ErrorMessage { get; set; }

        private string ErrorCode { get; set; }

        public TransactionException(string errorCode, string errorMessage)
        {
            this.ErrorCode = errorCode;
            this.ErrorMessage = errorMessage;
        }
    }
}
