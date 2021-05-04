using System.Collections.Generic;
using MyStagram.Core.Models.Helpers.Error;
using MyStagram.Core.Models.Mongo;

namespace MyStagram.Core.Logic.Responses.Query.Admin
{
    public class GetLogsResponse : BaseResponse
    {
        public List<LogDocument> Logs { get; set; }

        public GetLogsResponse(Error error = null) : base(error) { }
    }
}