using System;
using MyStagram.Core.Helpers;

namespace MyStagram.Core.Exceptions
{
    public class NoPermissionsException : Exception
    {
        public string ErrorCode { get; }

        public NoPermissionsException(string message, string errorCode = ErrorCodes.PermissionDenied) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}