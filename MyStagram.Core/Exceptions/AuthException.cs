using System;
using MyStagram.Core.Helpers;

namespace MyStagram.Core.Exceptions
{
    public class AuthException : Exception
    {
        public string ErrorCode { get; }

        public AuthException(string message, string errorCode = ErrorCodes.AuthError) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}