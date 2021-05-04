using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using MyStagram.Core.Extensions;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Query.Admin;
using MyStagram.Core.Logic.Responses.Query.Admin;
using MyStagram.Core.Models.Mongo;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Core.Logic.Handlers.Query.Admin
{
    public class GetLogsQuery : IRequestHandler<GetLogsRequest, GetLogsResponse>
    {
        private readonly IReadOnlyLogManager logManager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public GetLogsQuery(IReadOnlyLogManager logManager, IHttpContextAccessor httpContextAccessor)
        {
            this.logManager = logManager;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<GetLogsResponse> Handle(GetLogsRequest request, CancellationToken cancellationToken)
        {
            var pagedLogs = await logManager.GetLogs(request);

            httpContextAccessor.HttpContext.Response.AddPagination(pagedLogs.CurrentPage, pagedLogs.PageSize, pagedLogs.TotalCount, pagedLogs.TotalPages);

            var logs = new List<LogDocument>(pagedLogs);

            return new GetLogsResponse { Logs = logs };
        }
    }
}