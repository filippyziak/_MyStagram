using System;
using MyStagram.Core.Builders.Interface;
using MyStagram.Core.Models.Mongo;

namespace MyStagram.Core.Builders
{
    public class LogBuilder : ILogBuilder
    {
        private readonly LogDocument log = new LogDocument();

        public ILogBuilder CreatedAt(DateTime date)
        {
            this.log.Date = date;

            return this;
        }

        public ILogBuilder SetLevel(string level)
        {
            this.log.Level = level;

            return this;
        }

        public ILogBuilder SetLogger(string logger)
        {
            this.log.Logger = logger;

            return this;
        }

        public ILogBuilder SetMessage(string message, string trace)
        {
            this.log.Message = message;
            this.log.Trace = trace;

            return this;
        }

        public ILogBuilder WithAction(string url, string action)
        {
            this.log.Url = url;
            this.log.Action = action;

            return this;
        }

        public LogDocument Build() => this.log;
    }
}