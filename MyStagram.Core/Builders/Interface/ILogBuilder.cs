using System;
using MyStagram.Core.Models.Mongo;

namespace MyStagram.Core.Builders.Interface
{
    public interface ILogBuilder : IBuilder<LogDocument>
    {
        ILogBuilder CreatedAt(DateTime date);
        ILogBuilder SetLevel(string level);
        ILogBuilder SetLogger(string logger);
        ILogBuilder SetMessage(string message, string trace);
        ILogBuilder WithAction(string url, string action);
    }
}