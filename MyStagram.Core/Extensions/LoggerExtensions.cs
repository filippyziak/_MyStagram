using MyStagram.Core.Logging;
using MyStagram.Core.Models.Helpers.Error;

namespace MyStagram.Core.Extensions
{
    public static class LoggerExtensions
    {
        public static void LogResponse(this INLogger logger, string message, Error error = null)
        {
            if (error != null)
                logger.Error(error.Message);
            else
                logger.Info(message);
        }
    }
}