using System;
using MyStagram.Core.Logging;
using NLog;

namespace MyStagram.Infrastructure.Logging
{
    public class NLogger : INLogger
    {
        private static readonly ILogger logger = NLog.LogManager.GetCurrentClassLogger();

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(exception, message);
        }
    }
}