using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MyStagram.Core.Builders;
using MyStagram.Core.Data.Mongo;
using MyStagram.Core.Enums;
using MyStagram.Core.Helpers;
using MyStagram.Core.Logging;
using MyStagram.Core.Logic.Requests.Query.Admin;
using MyStagram.Core.Models.Helpers.Pagination;
using MyStagram.Core.Models.Mongo;
using MyStagram.Core.Services.Interfaces;

namespace MyStagram.Infrastructure.Logging
{

    public class LogManager : ILogManager
    {
        private readonly IMongoRepository<LogDocument> logsMongoRepository;
        private readonly IFilesService filesService;

        public LogManager(IMongoRepository<LogDocument> logsMongoRepository, IFilesService filesService)
        {
            this.logsMongoRepository = logsMongoRepository;
            this.filesService = filesService;
        }

        public async Task<bool> StoreLogs()
        {
            string logsFileRelativePath = $"logs/api-logs-{DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")}.log";
            string logsFilePath = $@"{filesService.WebRootPath}/{logsFileRelativePath}";

            if (!filesService.FileExists(logsFilePath))
                return false;

            var allLogs = LoadLogsFromFile(logsFilePath);

            foreach (var log in from fileLog in allLogs
                                let logProps = fileLog.Split("$|")
                                let log = new LogBuilder()
                                    .CreatedAt(DateTime.Parse(logProps[0]))
                                    .SetLevel(logProps[1])
                                    .SetLogger(logProps[2])
                                    .SetMessage(logProps[3], logProps[4])
                                    .WithAction(logProps[5], logProps[6])
                                    .Build()
                                select log)
                await logsMongoRepository.Insert(log);

            filesService.Delete(logsFileRelativePath);

            return true;
        }

        public async Task ClearLogs()
        {
            var logsToDelete = (await logsMongoRepository.GetAll()).ToList()
                .Where(l => ((l.Level == Constants.INFO || l.Level == Constants.DEBUG) && l.Date.AddMonths(Constants.InfoLogLifeTimeInMonths) < DateTime.Now)
                    || (l.Level == Constants.WARNING && l.Date.AddMonths(Constants.WarningLogLifeTimeInMonths) < DateTime.Now)
                    || (l.Level == Constants.ERROR && l.Date.AddMonths(Constants.ErrorLogLifeTimeInMonths) < DateTime.Now))
                .ToList();

            for (int i = 0; i < logsToDelete.Count; i++)
                await logsMongoRepository.Delete(logsToDelete[i].Id.ToString());
        }

        public async Task<PagedList<LogDocument>> GetLogs(GetLogsRequest paginationRequest)
        {
            var logs = (await logsMongoRepository.GetAll()).ToList().Where(l => l.Date >= paginationRequest.MinDate
                && l.Date <= paginationRequest.MaxDate);

            if (!string.IsNullOrEmpty(paginationRequest.Level))
                logs = logs.Where(l => l.Level.ToLower() == paginationRequest.Level.ToLower());

            if (!string.IsNullOrEmpty(paginationRequest.Message))
                logs = logs.Where(l => l.Message.ToLower().Contains(paginationRequest.Message.ToLower()));

            if (!string.IsNullOrEmpty(paginationRequest.Url))
                logs = logs.Where(l => l.Url.ToLower().Contains(paginationRequest.Url.ToLower()));

            if (!string.IsNullOrEmpty(paginationRequest.Action))
                logs = logs.Where(l => l.Action.ToLower().Contains(paginationRequest.Action.ToLower()));

            switch (paginationRequest.SortType)
            {
                case SortType.Descending:
                    logs = logs.OrderByDescending(l => l.Date);
                    break;
                case SortType.Ascending:
                    logs = logs.OrderBy(l => l.Date);
                    break;
                default: break;
            }

            return PagedList<LogDocument>.Create(logs, paginationRequest.PageNumber, paginationRequest.PageSize);
        }

        private IEnumerable<string> LoadLogsFromFile(string logsFilePath)
            => File.ReadAllText(logsFilePath).Replace("\r\n", "").Split("$#").Skip(1);
    }
}