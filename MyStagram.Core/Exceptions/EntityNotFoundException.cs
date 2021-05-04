using System;
using MyStagram.Core.Helpers;

namespace MyStagram.Core.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public string ErrorCode { get; }

        public EntityNotFoundException(string message, string errorCode = ErrorCodes.EntityNotFound) : base(message)
        {
              ErrorCode = errorCode;  
        }
    }
}