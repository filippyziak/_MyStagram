using System;
using MyStagram.Core.Helpers;

namespace MyStagram.Core.Exceptions
{
    public class BlockException : Exception
    {
        public string ErrorCode { get; }
        public BlockException(string message, string errorCode = ErrorCodes.AccountBlocked) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}