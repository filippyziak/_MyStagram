namespace MyStagram.Core.Models.Helpers.Error
{
    public class Error
    {
        public string ErrorCode { get; private set; }
        public string Message { get; private set; }

        public static Error Build(string errorCode, string message) => new Error { ErrorCode = errorCode, Message = message };
    }
}